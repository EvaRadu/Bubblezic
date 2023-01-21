module.exports = listBalles = [


     // TOUCHER PROLONGER
   { 
        id:0,
        typeName : "bubble",
        posX: 2,
        posY: 0,
        idTrajectoire: 1,
        couleur: 'magenta',
        rayon: 1,
        temps: 1,
        type: 1,
        duration: 4,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:2,
        typeName : "bubble",
        idTrajectoire: 1,
        posX: 5,
        posY: 0,
        couleur: 'grey',
        rayon: 1,
        temps: 1,
        type: 9,
        duration: 4,
        rotation: 90,
        side: 0,
        texture: "cibleToucherProlonger"
    },
    { 
        id:1,
        idBubble : 1,
        idCible : 2,
        typeName : "trajectory",
        posX: 4,
        posY: 0,
        couleur: 'blue',
        temps: 1,
        duration: 4, 
        width : 5,
        height : 3,
    },

      
    // MALUS
    { 
            id:3,
            typeName : "bubble",
            type: 4,

            posX: -2,
            posY: -2,
            posXOpponent : 0,
            posYOpponent : 0,
            impulsion: 0.5,

            couleur: 'white',
            rayon: 2,
            temps: 5,
            duration: 3,
            rotation: 90,
            side: 0,
            freezeDuration : 7,
            texture: "flocon"
        },

        { 
            id:4,
            typeName : "bubble",
            type: 5,

            posX: -4,
            posY: 3,
            posXOpponent : 0,
            posYOpponent : 0,
            impulsion: 0.5,

            couleur: 'green',
            rayon: 2,
            temps: 8,
            duration: 3,
            rotation: 90,
            side: 0,
            nbMalusMultiple : 3,
            texture: ""
        },
    // BULLE SIMPLE nÂ°2
           { 
            id:5,
            typeName : "bubble",
            posX: 2,
            posY: 2,
            couleur: 'red',
            rayon: 3,
            temps: 8,
            type: 0,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: "bubble2"
        },
    
    // PUZZLE
    { 
        id:6,
        typeName : "bubble",
        posX: -6,
        posY: 0,
        couleur: 'white',
        rayon: 3,
        temps: 11,
        type: 7,
        duration: 3,
        rotation: 90,
        side: 1,
        texture: ""
        },
        { 
        id:7,
        typeName : "bubble",
        posX: 6,
        posY: 0,
        couleur: 'white',
        rayon: 3,
        temps: 11,
        type: 7,
        duration: 3,
        rotation: -90,
        side: 2,
        texture: ""
    },
    { 
        id:8,
        typeName : "bubble",
        posX: 0,
        posY: 0,
        couleur: 'magenta',
        rayon: 3,
        temps: 11,
        type: 6,
        duration: 3,
        rotation: 90,
        side: 0,
        texture: "ciblePuzzle"
    },

    // DEUX BALLES EN MEME TEMPS
    { 
            id:9,
            typeName : "bubble",
            posX: 3,
            posY: 3,
            couleur: 'black',
            rayon: 3,
            temps: 14,
            type: 0,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: ""
        },
        { 
            id:10,
            typeName : "bubble",
            posX: -3,
            posY: -3,
            couleur: 'black',
            rayon: 3,
            temps: 14,
            type: 0,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: ""
        },

        // DEUXIEME PUZZLE
        { 
            id:11,
            typeName : "bubble",
            posX: -6,
            posY: 3,
            couleur: 'white',
            rayon: 3,
            temps: 17,
            type: 7,
            duration: 3,
            rotation: 90,
            side: 1,
            texture: ""
            },
            { 
            id:12,
            typeName : "bubble",
            posX: 6,
            posY: -3,
            couleur: 'white',
            rayon: 3,
            temps: 17,
            type: 7,
            duration: 3,
            rotation: -90,
            side: 2,
            texture: ""
        },
        { 
            id:13,
            typeName : "bubble",
            posX: 0,
            posY: 0,
            couleur: 'white',
            rayon: 3,
            temps: 17,
            type: 6,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: "ciblePuzzle"
        },

        // QUATRES BULLES

     { 
            id:14,
            typeName : "bubble",
            posX: -4,
            posY: 3,
            couleur: 'green',
            rayon: 2,
            temps: 20,
            type: 0,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: ""
        },
        { 
            id:15,
            typeName : "bubble",
            posX: -4,
            posY: -3,
            couleur: 'blue',
            rayon: 2,
            temps: 20,
            type: 0,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: ""
        },
        { 
            id:16,
            typeName : "bubble",
            posX: 4,
            posY: 3,
            couleur: 'green',
            rayon: 2,
            temps: 20,
            type: 0,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: ""
        },
        { 
            id:17,
            typeName : "bubble",
            posX: 4,
            posY: -3,
            couleur: 'blue',
            rayon: 2,
            temps: 20,
            type: 0,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: ""
        },

];

/*
module.exports = listBalles = [

        // BULLE SIMPLE
        { 
            id:0,
            typeName : "bubble",
            posX: -2,
            posY: -2,
            couleur: 'blue',
            rayon: 3,
            temps: 0,
            type: 0,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: ""
        },

    // BULLE SIMPLE nÂ°2
           { 
            id:1,
            typeName : "bubble",
            posX: 2,
            posY: 2,
            couleur: 'magenta',
            rayon: 3,
            temps: 3,
            type: 0,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: ""
        },
    
    // BULLE SIMPLE
    { 
            id:2,
            typeName : "bubble",
            posX: -2,
            posY: -2,
            couleur: 'yellow',
            rayon: 3,
            temps: 6,
            type: 0,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: ""
        },

    // BULLE SIMPLE nÂ°2
           { 
            id:3,
            typeName : "bubble",
            posX: 2,
            posY: 2,
            couleur: 'red',
            rayon: 3,
            temps: 9,
            type: 0,
            duration: 3,
            rotation: 90,
            side: 0,
            texture: ""
        }

];*/