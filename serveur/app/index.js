const WebSocket = require('ws');
const clients = new Map();

const wss = new WebSocket.Server({ port: 8080 },()=>{
    console.log("Server started");
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

wss.on('listening',()=>{
    console.log('server listening on 8080')
});

function uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
};
