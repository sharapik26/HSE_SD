import React, { useState, useEffect, useCallback } from 'react';
import axios from 'axios';
import {
  AppBar, Toolbar, Typography, Container, Grid, Card, CardContent, Button,
  CircularProgress, Box, Chip, Modal, TextField, Snackbar, Alert
} from '@mui/material';
import AccountBalanceWalletIcon from '@mui/icons-material/AccountBalanceWallet';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import './App.css';

const apiClient = axios.create({
  baseURL: 'http://localhost:8000/gateway',
});

const statusConfig = {
  New: { text: 'pending...', className: 'new' },
  Finished: { text: 'finished', className: 'finished' },
  Cancelled: { text: 'cancelled', className: 'cancelled' },
};

function App() {
  const [userId, setUserId] = useState(null);
  const [balance, setBalance] = useState(0);
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState({ balance: true, orders: true });
  const [depositModalOpen, setDepositModalOpen] = useState(false);
  const [orderModalOpen, setOrderModalOpen] = useState(false);
  const [depositAmount, setDepositAmount] = useState('');
  const [newOrder, setNewOrder] = useState({ description: '', amount: '' });
  const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' });

  const showSnackbar = (message, severity = 'success') => {
    setSnackbar({ open: true, message, severity });
  };

  const getBalance = useCallback(async (currentUserId) => {
    if (!currentUserId) return;
    setLoading(prev => ({ ...prev, balance: true }));
    try {
      const response = await apiClient.get(`/payments/accounts/balance/${currentUserId}`);
      setBalance(response.data.balance);
    } catch (error) {
      if (error.response && error.response.status === 404) {
        try {
          await apiClient.post('/payments/accounts', { userId: currentUserId });
          setBalance(0);
        } catch (creationError) {
          if (creationError.response && creationError.response.status !== 409) {
            showSnackbar('Ошибка при создании счета', 'error');
          }
        }
      } else {
        showSnackbar('Ошибка сети при получении баланса', 'error');
      }
    } finally {
      setLoading(prev => ({ ...prev, balance: false }));
    }
  }, []);

  const getOrders = useCallback(async (currentUserId) => {
    if (!currentUserId) return;
    setLoading(prev => ({ ...prev, orders: true }));
    try {
      const response = await apiClient.get(`/orders/user/${currentUserId}`);
      setOrders(response.data);
    } catch (error) {
      if (error.response && error.response.status === 404) {
        setOrders([]);
      } else {
        showSnackbar('Ошибка сети при получении заказов', 'error');
      }
    } finally {
      setLoading(prev => ({ ...prev, orders: false }));
    }
  }, []);

  const handleDeposit = async () => {
    if (!depositAmount || parseFloat(depositAmount) <= 0) {
      showSnackbar('Введите корректную сумму для пополнения', 'warning');
      return;
    }
    try {
      await apiClient.post('/payments/accounts/deposit', { userId, amount: parseFloat(depositAmount) });
      showSnackbar('Счет успешно пополнен!', 'success');
      setDepositModalOpen(false);
      setDepositAmount('');
      await getBalance(userId);
    } catch (error) {
      showSnackbar('Ошибка при пополнении счета', 'error');
    }
  };

  const handleCreateOrder = async () => {
    if (!newOrder.description || !newOrder.amount || parseFloat(newOrder.amount) <= 0) {
        showSnackbar('Заполните все поля для создания заказа', 'warning');
        return;
    }
    try {
      await apiClient.post('/orders', {
        userId,
        description: newOrder.description,
        amount: parseFloat(newOrder.amount)
      });
      showSnackbar('Заказ успешно создан! Статус обновится через несколько секунд.', 'info');
      setOrderModalOpen(false);
      setNewOrder({ description: '', amount: '' });
      await getOrders(userId);
    } catch (error) {
      showSnackbar('Ошибка при создании заказа', 'error');
    }
  };

  useEffect(() => {
    let storedUserId = localStorage.getItem('asyncShopUserId');
    if (!storedUserId) {
      storedUserId = crypto.randomUUID();
      localStorage.setItem('asyncShopUserId', storedUserId);
    }
    setUserId(storedUserId);
  }, []);

  useEffect(() => {
    if (userId) {
      const interval = setInterval(() => {
        getBalance(userId);
        getOrders(userId);
      }, 5000);

      getBalance(userId);
      getOrders(userId);

      return () => clearInterval(interval);
    }
  }, [userId, getBalance, getOrders]);
  
  const getStatusInfo = (status) => {
    return statusConfig[status] || { text: 'unknown', className: '' };
  };

  return (
    <>
      <AppBar position="static">
        <Toolbar>
          <ShoppingCartIcon sx={{ mr: 2 }}/>
          <Typography variant="h6">AhbazanShop</Typography>
        </Toolbar>
      </AppBar>

      <Container sx={{ mt: 4, mb: 4 }}>
        <Grid container spacing={4}>
          <Grid item xs={12} md={4}>
            <Card>
              <CardContent>
                <Typography variant="h5" gutterBottom>Ваш профиль</Typography>
                <Typography variant="body2" color="text.secondary">User ID:</Typography>
                <Typography variant="subtitle1" gutterBottom sx={{ wordBreak: 'break-all', fontSize: '0.9rem' }}>{userId}</Typography>
                <Box sx={{ display: 'flex', alignItems: 'center', mt: 2 }}>
                  <AccountBalanceWalletIcon sx={{ mr: 1, color: 'primary.main' }}/>
                  <Typography variant="h4">
                    {loading.balance && balance === 0 ? <CircularProgress size={32} /> : `${(balance || 0).toFixed(2)} ₽`}
                  </Typography>
                </Box>
                <Button variant="contained" fullWidth sx={{ mt: 3 }} onClick={() => setDepositModalOpen(true)} startIcon={<AddCircleOutlineIcon/>}>
                  Пополнить счет
                </Button>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12} md={8}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
              <Typography variant="h5">Ваши заказы</Typography>
              <Button variant="outlined" onClick={() => setOrderModalOpen(true)} startIcon={<AddCircleOutlineIcon/>}>
                Создать заказ
              </Button>
            </Box>
            <Box sx={{ maxHeight: '60vh', overflowY: 'auto', pr: 1 }}>
              {loading.orders && orders.length === 0 ? (<CircularProgress />) :
               orders.length > 0 ? (orders.map(order => {
                const statusInfo = getStatusInfo(order.status);
                return (
                    <Card key={order.id} sx={{ mb: 2 }}>
                    <CardContent>
                        <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                        <Typography variant="body2" color="text.secondary" sx={{ wordBreak: 'break-all', mr: 1, fontSize: '0.8rem' }}>{order.id}</Typography>
                        <Chip label={statusInfo.text} size="small" className={`status-chip ${statusInfo.className}`} />
                        </Box>
                        <Typography variant="h6">{order.description}</Typography>
                        <Typography variant="body1">{(order.amount || 0).toFixed(2)} ₽</Typography>
                    </CardContent>
                    </Card>
                );
               })) : (<Typography>У вас пока нет заказов.</Typography>)}
            </Box>
          </Grid>
        </Grid>
      </Container>
      
      <Modal open={depositModalOpen} onClose={() => setDepositModalOpen(false)}>
        <Box sx={modalStyle}>
          <Typography variant="h6" component="h2">Пополнение счета</Typography>
          <TextField fullWidth label="Сумма" type="number" variant="outlined" margin="normal" value={depositAmount} onChange={(e) => setDepositAmount(e.target.value)}/>
          <Button variant="contained" onClick={handleDeposit}>Пополнить</Button>
        </Box>
      </Modal>

      <Modal open={orderModalOpen} onClose={() => setOrderModalOpen(false)}>
        <Box sx={modalStyle}>
          <Typography variant="h6" component="h2">Новый заказ</Typography>
          <TextField fullWidth label="Описание" variant="outlined" margin="normal" value={newOrder.description} onChange={(e) => setNewOrder({...newOrder, description: e.target.value})}/>
          <TextField fullWidth label="Сумма" type="number" variant="outlined" margin="normal" value={newOrder.amount} onChange={(e) => setNewOrder({...newOrder, amount: e.target.value})}/>
          <Button variant="contained" onClick={handleCreateOrder}>Создать</Button>
        </Box>
      </Modal>
      
      <Snackbar open={snackbar.open} autoHideDuration={6000} onClose={() => setSnackbar({...snackbar, open: false})} anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}>
        <Alert onClose={() => setSnackbar({...snackbar, open: false})} severity={snackbar.severity} sx={{ width: '100%' }}>{snackbar.message}</Alert>
      </Snackbar>
    </>
  );
}

const modalStyle = {
  position: 'absolute', top: '50%', left: '50%', transform: 'translate(-50%, -50%)',
  width: 400, bgcolor: 'background.paper', border: '2px solid #000', boxShadow: 24, p: 4,
};

export default App;
