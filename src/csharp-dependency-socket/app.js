var io = require('socket.io')(8081)

const requestItem = require('./entity/requestItem')

var requestArr = [];

io.on("connection", (socket) => {
    console.log('Connecteded:', socket.id);

    socket.on('disconnect', () => {
        console.log('Disconnected:', socket.id);
        let items = requestArr.filter(x => x.socketId === socket.id);
        items.forEach((item) => {
            let indexOf = requestArr.indexOf(item);
            requestArr.splice(indexOf,1);
        })        
    });

    socket.on('visualizedependency', (repo) => {
        const _reqItem = new requestItem();
        _reqItem.socketId = socket.id;
        _reqItem.repoId = repo.id;
        io.emit("_visualizedependency", repo);
        requestArr.push(_reqItem);
    })

    socket.on('showDependency', (repo,dependencies) => {
        let clients = requestArr.filter(x => x.repoId === repo.id);
        clients.forEach((item) => {            
            io.emit("_showDependency", repo, dependencies);
            console.log(`${item.socketId} Published ${repo.name} Project Dependencies`);
            let indexOf = requestArr.indexOf(item);
            requestArr.splice(indexOf,1);
        });        
    })
})

io.httpServer.on('listening', function () {
    console.log('listening on port', io.httpServer.address().port);
})