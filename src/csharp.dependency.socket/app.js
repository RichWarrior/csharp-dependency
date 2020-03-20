var io = require('socket.io')(8081)

io.on("connection", (socket) => {
    console.log('Connected:', socket.id);
})

io.on('disconnect', (reason) => {
    console.log(reason)
});

io.httpServer.on('listening', function () {
    console.log('listening on port', io.httpServer.address().port);
})