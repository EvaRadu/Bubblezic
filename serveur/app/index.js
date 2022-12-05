const WebSocket = require('ws');
const uuidv4 = require('./fonctions/generateId.js');

const myport = 8080;
const clients = new Map();

const wss = new WebSocket.Server({ port: myport },()=>{
    console.log("Server started");
});

wss.on('listening',()=>{
    console.log('server listening on port', myport);
});

wss.on('connection', (ws) => {
    const id = uuidv4();
    const color = Math.floor(Math.random() * 360);
    const metadata = {id, color};

    clients.set(ws, metadata);
    console.log("New client connected\n");

    ws.on('message', (messageAsString) => {
        console.log("message received : \n");
        console.log(messageAsString.toString());
        ws.send(messageAsString.toString());





    });

    ws.on("close", () => {
        clients.delete(ws);
        console.log("Client removed");
    });
});

