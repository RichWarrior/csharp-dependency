var io = require('socket.io')(8081)

io.on("connection", (socket) => {
    console.log('Connecteded:', socket.id);

    socket.on('disconnect', () => {
        console.log('Disconnected:', socket.id);
    });

    socket.on('visualizedependency', (repo) => {
        console.log(repo);
    })
})

io.httpServer.on('listening', function () {
    console.log('listening on port', io.httpServer.address().port);
})