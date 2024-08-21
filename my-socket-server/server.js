const express = require('express');
const https = require('https');
const fs = require('fs');
const socketIo = require('socket.io');
const cors = require('cors');

const app = express();

// Cargar certificados SSL
const options = {
  key: fs.readFileSync('C:/Users/Ronnie/Downloads/server.key'), // Ruta al archivo .key
  cert: fs.readFileSync('C:/Users/Ronnie/Downloads/server.crt') // Ruta al archivo .crt
};

const server = https.createServer(options, app);
const io = socketIo(server, {
  cors: {
    origin: "*", // Cambia esto por el origen de tu cliente
    methods: ["GET", "POST"]
  }
});

app.use(cors());
app.use(express.json());

app.use((err, req, res, next) => {
  console.error('Error occurred:', err);
  res.status(500).send('Something went wrong');
});


// Ruta básica para verificar que el servidor responde
app.get('/', (req, res) => {
  res.send('Hello World');
});


app.post('/send-message', (req, res) => {
  const { message, roomId } = req.body;
  io.to(roomId).emit('message', { text: message, roomId, user: false });
  res.sendStatus(200);
});

io.on('connection', (socket) => {
  console.log('New client connected');

  socket.on('join', (roomId) => {
    socket.join(roomId);
  });

  socket.on('leave', (roomId) => {
    socket.leave(roomId);
  });

  socket.on('message', (data) => {
    io.to(data.roomId).emit('message', data);
  });

  socket.on('disconnect', () => {
    console.log('Client disconnected');
  });
});

server.listen(8443, () => {
  console.log('Servidor HTTPS en funcionamiento en el puerto 8443');
});
