const WebSocket = require('ws');
const uuidv4 = require('./fonctions/generateId');
const wait = require('./fonctions/wait');
const calculePoints = require('./fonctions/calculerPoints');
const calculePos = require('./fonctions/calculerPosition');
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
        console.log("message received : " + messageAsString.toString());

        /* -------------------------- */
        /* --- MESSAGE = 'Ready ' --- */
        /* -------------------------- */
        if(messageAsString.toString() == 'Ready'){
            /*nbClients++;
            while(nbClients < 2 && nbClients >= 0){
                console.log("waiting for second client");
                await wait(1000);
            }*/
            console.log("Both clients are ready, sending balls");
            listBalles.forEach(ball => {
                ws.send(JSON.stringify(ball));
            });
        }

        /* -------------------------------- */
        /* --- MESSAGE = 'Update Score' --- */
        /* -------------------------------- */
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

        /* -------------------------------- */
        /* --- MSG= 'Freeze Malus Sent' --- */
        /* -------------------------------- */
        else if (messageAsString.toString().includes('Freeze Malus Sent.')) {
            // get the second client
            // send malus data to the second client

            //bubble to delete
            let pos1 = messageAsString.toString().indexOf('=');
            let pos2 = messageAsString.toString().indexOf(',');
            let msg = messageAsString.toString().substring(pos1+1, pos2);
            let bubbleToDelete = "Opponent " + msg;

            //posX
            msg = messageAsString.toString().substring(pos1 + 1);

            pos1 = msg.toString().indexOf('=');
            pos2 = msg.toString().indexOf(',');
            msg = msg.toString().substring(pos1 + 1);
        
            let posX = parseFloat(msg.replace(",", "."));

            //posY
            pos1 = msg.toString().indexOf('=');
            pos2 = msg.toString().indexOf(',');
            msg = msg.toString().substring(pos1 + 1);
            let posY = parseFloat(msg.replace(",", "."));

            //freezeDuration
            pos1 = msg.toString().indexOf('=');
            msg = msg.toString().substring(pos1 + 1);
            let freezeDuration = parseFloat(msg.replace(",", "."));

            for(let [key, value] of clients){
                if(value.id == metadata.id){
                    key.send('Freeze Malus Received with duration = ' + freezeDuration);
                    key.send('Delete Bubble = ' + bubbleToDelete);
                }
            }
        }

        /* -------------------------------- */
        /* --- MSG= 'Multiple Malus Sent' --- */
        /* -------------------------------- */
        else if (messageAsString.toString().includes('Freeze Malus Sent.')) {
            // get the second client
            // send malus data to the second client

            //bubble to delete
            let pos1 = messageAsString.toString().indexOf('=');
            let pos2 = messageAsString.toString().indexOf(',');
            let msg = messageAsString.toString().substring(pos1+1, pos2);
            let bubbleToDelete = "Opponent " + msg;

            //posX
            msg = messageAsString.toString().substring(pos1 + 1);

            pos1 = msg.toString().indexOf('=');
            pos2 = msg.toString().indexOf(',');
            msg = msg.toString().substring(pos1 + 1);
        
            let posX = parseFloat(msg.replace(",", "."));

            //posY
            pos1 = msg.toString().indexOf('=');
            pos2 = msg.toString().indexOf(',');
            msg = msg.toString().substring(pos1 + 1);
            let posY = parseFloat(msg.replace(",", "."));

            //id
            pos1 = msg.toString().indexOf('=');
            msg = msg.toString().substring(pos1 + 1);
            let id = parseFloat(msg.replace(",", "."));

            for(let [key, value] of clients){
                if(value.id == metadata.id){
                    key.send('Multiple Malus Received with id = ' + id);
                    key.send('Delete Bubble = ' + bubbleToDelete);
                }
            }
        }


        /* --------------------------------- */
        /* --- MESSAGE = 'Delete Bubble' --- */
        /* --------------------------------- */
        else if (messageAsString.toString().includes('Delete Bubble.')) {
            let pos1 = messageAsString.toString().indexOf('=');
            let msg = messageAsString.toString().substring(pos1+1, messageAsString.toString().length);
            let bubbleToDelete = "Opponent " + msg;
            for(let [key, value] of clients){
                if(value.id != metadata.id){
                    key.send('Delete Bubble = ' + bubbleToDelete);
                }
            }
        }

        /* ------------------------------- */
        /* --- MESSAGE = 'Move Circle' --- */
        /* ------------------------------- */
        else if(messageAsString.toString().includes('Move Circle')){
            let pos1 = messageAsString.toString().indexOf('=');
            let pos2 = messageAsString.toString().indexOf(',');
            let msg = messageAsString.toString().substring(pos1+1, pos2);
            let bubbleToMove = "Opponent " + msg;

            msg = messageAsString.toString().substring(pos1 + 1);

            pos1 = msg.toString().indexOf('=');
            pos2 = msg.toString().indexOf(',');
            msg = msg.toString().substring(pos1 + 1);
        
            let posX = parseFloat(msg.replace(",", "."));

            pos1 = msg.toString().indexOf('=');
            msg = msg.toString().substring(pos1 + 1);
            let posY = parseFloat(msg.replace(",", "."));

            //console.log("name = " + bubbleToMove + "posX = " + posX + " posY = " + posY);

            let newPos = calculePos(posX, posY);
            for(let [key, value] of clients){
                if(value.id != metadata.id){
                    key.send('Move Circle = ' + bubbleToMove + ', posX = ' + newPos[0] + ', posY = ' + newPos[1]);
                }
            }

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