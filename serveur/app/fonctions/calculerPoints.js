const listBalles = require('../objects/balls');
const listBalles2 = require('../objects/balls2');
const listBallesDemo = require('../objects/ballsDemo');


module.exports = function calculePoints(time, id) {
    let ballTime = 0;
    let delay = 1.2;

    // get the demoMode value of the index.js file
    let demoMode = require('../index').demoMode;
    let currentJson;

    if(demoMode){
        currentJson = listBallesDemo;
    }
    else{
        currentJson = listBalles2;
    }

    currentJson.forEach(ball => {
        if (ball.id == id) {
            ballTime = ball.temps;
            ballDuration = ball.duration;
        }
    });

    let timeBubble = ballTime + ballDuration;
    
    //console.log("ballTime =" + ballTime);
    //console.log("ballDuration =" + ballDuration);
    console.log("time = " +time);
    console.log("timeBubble = " +timeBubble);

    if(Math.abs(time-timeBubble) <= 0.05 ){
        return 10;
    }
    else if(Math.abs(time-timeBubble) <= 0.1 ){
        return 9;
    }
    else if(Math.abs(time-timeBubble) <= 0.2 ){
        return 8;
    }
    else if(Math.abs(time-timeBubble) <= 0.3 ){
        return 7;
    }
    else if(Math.abs(time-timeBubble) <= 0.4 ){
        return 6;
    }
    else if(Math.abs(time-timeBubble) <= 1 ){
        return 5;
    }
    else if(Math.abs(time-timeBubble) <= 1.5 ){
        return 4;
    }
    else if(Math.abs(time-timeBubble) <= 2 ){
        return 3;
    }
    else if(Math.abs(time-timeBubble) <= 2.5 ){
        return 2;
    }
    else if(Math.abs(time-timeBubble) <= 3 ){
        return 1;
    }
    else{
        return 0;
    }


}