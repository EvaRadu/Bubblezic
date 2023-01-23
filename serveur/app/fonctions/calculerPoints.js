const listBalles = require('../objects/balls');

module.exports = function calculePoints(time, id, type, bonus, bonusPoint) {
    let ballTime = 0;
    let delay = 1.2;

    listBalles.forEach(ball => {
        if (ball.id == id) {
            ballTime = ball.temps;
            ballDuration = ball.duration;
        }
    });

    let timeBubble = ballTime + ballDuration;
    
    //console.log("time = " +time);
    //console.log("timeBubble = " +timeBubble);

    if (type != 10) {
    //[point, bool bonus, numBonus]
        if(Math.abs(time-timeBubble) <= 0.05 ){
            bonusPoint += 1;
            return [10, true, bonusPoint ];
        }
        else if(Math.abs(time-timeBubble) <= 0.1 ){
            bonusPoint += 1;

            return [9, true, bonusPoint];
        }
        else if(Math.abs(time-timeBubble) <= 0.2 ){
            bonusPoint += 1;

            return [8, true, bonusPoint];
        }
        else if(Math.abs(time-timeBubble) <= 0.3 ){
            bonusPoint += 1;
            return  [7, true, bonusPoint];
        }
        else if(Math.abs(time-timeBubble) <= 0.4 ){
            bonusPoint += 1;
            return  [6, true, bonusPoint];
        }
        else if(Math.abs(time-timeBubble) <= 1 ){
            bonusPoint += 1;
            return  [5, true, bonusPoint];
        }
        else if(Math.abs(time-timeBubble) <= 1.5) {
            bonusPoint += 1;
            return  [4, true, bonusPoint];
        }
        else if(Math.abs(time-timeBubble) <= 2 ){
            return  [3, false, 0];
        }
        else if(Math.abs(time-timeBubble) <= 2.5 ){
            return [2, false, 0];
        }
        else if(Math.abs(time-timeBubble) <= 3 ){
            return [1, false, 0];
        }
        else{
            return [0, false, 0];
        }
    } else {
        return [-5,false,0];
    }


}