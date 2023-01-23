const WebSocket = require('ws');
const uuidv4 = require('./fonctions/generateId');
const wait = require('./fonctions/wait');
const calculePoints = require('./fonctions/calculerPoints');
const calculePos = require('./fonctions/calculerPosition');
const listBalles = require('./objects/balls');
const listBallesMusic1 = require('./objects/ballsMusic1');
const listBallesMusic2 = require('./objects/ballsMusic2');
const listBallesDemoMusic = require('./objects/ballsDemoMusic');
const myport = 8080;
const clients = new Map();

let nbClients = 0;
let log = [];
let demoMode = false;
let music = 1;

const wss = new WebSocket.Server({ port: myport },()=>{
    console.log("Server started");
});

wss.on('listening',()=>{
    console.log('server listening on port', myport);
});

wss.on('connection', (ws) => {
    const id = uuidv4();
    var score = 0;
    var bonus = false;
    var bonusPoint = 0;
    var metadata = {id, score, bonus, bonusPoint};
    //console.log(" id = " + metadata.id + " score = " + metadata.score + "");

    clients.set(ws, metadata);
    console.log("New client connected\n");
    //console.log("SIZE : " + clients.size + " ");
    //console.log("CLIENTS : " + clients + " ");

    ws.on('message', async (messageAsString) => {
        console.log("message received : " + messageAsString.toString());


        /* ------------------------------- */
        /* --- MESSAGE = 'Ready Demo ' --- */
        /* ------------------------------- */
        if(messageAsString.toString() == 'Ready Demo'){
            nbClients++;
            while(nbClients < 2 && nbClients >= 0){
                console.log("waiting for second client");
                await wait(1000);
            }
            listBallesDemoMusic.forEach(ball => {
                ws.send(JSON.stringify(ball));
            });

            demoMode = true;
        }


        /* -------------------------- */
        /* --- MESSAGE = 'Ready ' --- */
        /* -------------------------- */
        else if(messageAsString.toString() == 'Ready'){
            nbClients++;
            while(nbClients < 2 && nbClients >= 0){
                console.log("waiting for second client");
                await wait(1000);
            }
            console.log("Both clients are ready, sending balls");
            if(music == 1){
            listBallesMusic1.forEach(ball => {
                ws.send(JSON.stringify(ball));
            });
            }
            else if(music == 2){
                listBallesMusic2.forEach(ball => {
                    ws.send(JSON.stringify(ball));
                });
            }            
        }

        /* -------------------------- */
        /* --- MESSAGE = 'Music' --- */
        /* -------------------------- */
        // Music = 1
        else if(messageAsString.toString().includes('Music')){
            console.log("Music");
            for(let [key, value] of clients){
                if(value.id != metadata.id){
                    key.send(messageAsString.toString());
                }
            }
            
            pos1 = messageAsString.toString().indexOf('=');
            music = parseInt(messageAsString.toString().substring(pos1+1));
        }


        /* ------------------------- */
        /* --- MESSAGE = 'Pause' --- */
        /* ------------------------- */
        else if(messageAsString.toString() == 'Pause'){
            //console.log("Pause");
            for(let [key, value] of clients){
                if(value.id != metadata.id){
                    key.send("Pause");
                }
            }
        }

        /* -------------------------- */
        /* --- MESSAGE = 'Resume' --- */
        /* -------------------------- */
        else if(messageAsString.toString() == 'Resume'){
            //console.log("Resume");
            for(let [key, value] of clients){
                if(value.id != metadata.id){
                    key.send("Resume");
                }
           }
        }

        /* -------------------------- */
        /* --- MESSAGE = 'Demo =' --- */
        /* -------------------------- */
        else if(messageAsString.toString().includes('Demo =')){
            let pos1 = messageAsString.toString().indexOf('=');
            let msg = messageAsString.toString().substring(pos1+1);
            for(let [key, value] of clients){
                if(value.id != metadata.id){
                    console.log("Demo = " + msg, "id = " + value.id);
                    key.send("Demo =" + msg);
                }
            }
        }

        /* ------------------------------- */
        /* --- MESSAGE = 'Start Scene" --- */
        /* ------------------------------- */
        else if(messageAsString.toString() == 'Start Scene'){
            //console.log("Start Scene");
            for(let [key, value] of clients){
                if(value.id != metadata.id){
                    key.send("Start Scene");
                }
            }
        }

        /* --------------------------- */
        /* --- MESSAGE = 'Scene 2" --- */
        /* --------------------------- */
        else if(messageAsString.toString() == 'Scene 2'){
            //console.log("Scene 2");
            for(let [key, value] of clients){
                if(value.id != metadata.id){
                    key.send("Scene 2");
                }
            }
        }

        /* ----------------------------- */
        /* --- MESSAGE = 'End Scene" --- */
        /* ----------------------------- */
        else if(messageAsString.toString() == 'End Scene'){
            let scoreTeam;
            let scoreOpponentTeam;

            for(let [key, value] of clients){
                if(value.id == metadata.id){
                    console.log("score = " + value.score);
                    scoreTeam = value.score;
                }
                else{
                    console.log("score = " + value.score);
                    scoreOpponentTeam = value.score;
                }
            }
            //console.log("ScoreTeam = " + scoreTeam + ", ScoreOpponent = " + scoreOpponentTeam);



            ws.send("ScoreTeam = " + scoreTeam + ", ScoreOpponent = " + scoreOpponentTeam);
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
            
            var point = calculePoints(temps, ballId, type, metadata.bonus, bonusPoint);
            bonus = point[1];
            metadata.bonus = bonus;
            bonusPoint = point[2];
            metadata.bonusPoint = bonusPoint;
            metadata.score += point[0];
            var p = point[0];
           
            console.log(point[0]);
           
            ws.send("New score = " + metadata.score + ", bonusStatus = " + metadata.bonus + ", bonusPoints = " + metadata.bonusPoint, ", points =" +p);

            // ON ENVOIT LE SCORE A L'AUTRE CLIENT
            for(let [key, value] of clients){
                if(value.id != metadata.id){
                    key.send("Opponent score = " + metadata.score + ", bonusStatus = " + metadata.bonus + ", bonusPoints = " + metadata.bonusPoint + ", points =" + p);
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
                if(value.id != metadata.id){
                    key.send('Freeze Malus Received with duration = ' + freezeDuration);
                    key.send('Delete Bubble = ' + bubbleToDelete);
                }
            }
        }

        /* -------------------------------- */
        /* -- MSG= 'Multiple Malus Sent' -- */
        /* -------------------------------- */
        else if (messageAsString.toString().includes('Multiple Malus Sent.')) {
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
                if(value.id != metadata.id){
                    key.send('Multiple Malus Received with id = ' + id);
                    //key.send('Delete Bubble = ' + bubbleToDelete);
                }
            }
        }

        /* --------------------------------- */
        /*  ---- MSG= Malus Received' ------ */
        /* -------------------------------- */
        else if (messageAsString.toString().includes('Multiple Malus Received') | messageAsString.toString().includes('Freeze Malus Received')) {
            metadata.bonus = false;
            metadata.bonusPoint = 0;
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