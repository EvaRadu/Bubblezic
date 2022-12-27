const WebSocket = require('ws');
const uuidv4 = require('./fonctions/generateId');
const wait = require('./fonctions/wait');
const calculePoints = require('./fonctions/calculerPoints');
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
    //console.log(" id = " + metadata.id + " score = " + metadata.score + "");

    clients.set(ws, metadata);
    console.log("New client connected\n");
    //console.log("SIZE : " + clients.size + " ");
    //console.log("CLIENTS : " + clients + " ");

    ws.on('message', async (messageAsString) => {
        if(messageAsString.toString() == 'Ready'){
            nbClients++;
            while(nbClients < 2 && nbClients >= 0){
                console.log("waiting for second client");
                await wait(1000);
            }
            console.log("Both clients are ready, sending balls");
            listBalles.forEach(ball => {
                ws.send(JSON.stringify(ball));
            });
        }
        else if (messageAsString.toString().includes('Update Score.')) {

            let pos1 = messageAsString.toString().indexOf('=');
            let pos2 = messageAsString.toString().indexOf(',');
            let msg = messageAsString.toString().substring(pos1+1, pos2);
            let ballId = parseInt(msg);


            msg = messageAsString.toString().substring(pos1 + 1);

            pos1 = msg.toString().indexOf('=');
            pos2 = msg.toString().indexOf(',');
            msg = msg.toString().substring(pos1 + 1);
        
            let temps = parseFloat(msg.replace(",", "."));
            console.log("temps = " + temps);

            pos1 = msg.toString().indexOf('=');
            msg = msg.toString().substring(pos1 + 1);
            let type = parseInt(msg);

            console.log("type = " + type);
            
            let point = calculePoints(temps, ballId, type);
        
            metadata.score += point;
            console.log(metadata.score);
           
            ws.send("New score = " + metadata.score);

            // ON ENVOIT LE SCORE A L'AUTRE CLIENT
            for(let [key, value] of clients){
                if(value.id != metadata.id){
                    key.send("Opponent score = " + metadata.score);
                }
            }
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