const express = require('express');
const http = require('http');
const socketIo = require('socket.io');
const cors = require('cors'); 

const app = express();
const server = http.createServer(app);
const io = socketIo(server, {
  cors: {
    origin: "http://localhost:8100", 
    methods: ["GET", "POST"]
  }
});

app.use(cors()); 
app.use(express.json());

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

server.listen(3000, () => {
  console.log('Listening on port 3000');
});
