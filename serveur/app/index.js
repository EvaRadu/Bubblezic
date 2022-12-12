const WebSocket = require('ws');
const uuidv4 = require('./fonctions/generateId.js');
const wait = require('./fonctions/wait.js');
const listBalles = require('./objects/balls');
const myport = 8080;
const clients = new Map();

let nbClients = 0;
let log = [];

const wss = new WebSocket.Server({ port: myport },()=>{
    console.log("Server started");
});

wss.on('listening',()=>{
    console.log('server listening on port', myport);
});

wss.on('connection', (ws) => {
    const id = uuidv4();
    const score = 0;
    const metadata = {id, score};

    clients.set(ws, metadata);
    console.log("New client connected\n");

    ws.on('message', async (messageAsString) => {
        if(messageAsString.toString() == 'Ready'){
            nbClients++;
            /*while(nbClients < 2){
                console.log("waiting for second client");
                await wait(1000);
            }*/
            //console.log("Both clients are ready");
            listBalles.forEach(ball => {
                //console.log(ball);
                ws.send(JSON.stringify(ball));
            })

        }
        else if (messageAsString.toString().includes('score')) {
            // calcul score
            let newScore = calculScore(messageAsString)
            // send new score
            ws.send(newScore);
        }
        else if (messageAsString.toString().includes('malus')) {
            // get the second client
            // send malus data to the second client
            ws.send('malus');
        }
        else{
            ws.send('Message not clear');
        }
    });

    ws.on("close", () => {
        clients.delete(ws);
        nbClients--;
        console.log("Client removed");
    });
});

