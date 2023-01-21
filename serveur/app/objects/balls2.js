module.exports = listBalles = [

    // BULLE SIMPLE 
   
    { 
        id:0,
        typeName : "bubble",
        posX: -1,
        posY: 1,
        couleur: 'blue',
        rayon: 2,
        temps: 0.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    // DEUX BULLES SIMPLES 
    { 
        id:1,
        typeName : "bubble",
        posX: 2,
        posY: 2,
        couleur: 'red',
        rayon: 2,
        temps: 1,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    { 
        id:2,
        typeName : "bubble",
        posX: -2,
        posY: -2,
        couleur: 'red',
        rayon: 2,
        temps: 1,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    // BULLE SIMPLE
    { 
        id:3,
        typeName : "bubble",
        posX: 3,
        posY: -2,
        couleur: 'yellow',
        rayon: 2,
        temps: 1.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    // 2 BULLES SIMPLES + TOUCHER PROLONGER
    { 
        id:4,
        typeName : "bubble",
        posX: -4,
        posY: 0,
        couleur: 'white',
        rayon: 2,
        temps: 3,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    { 
        id:5,
        typeName : "bubble",
        posX: 0,
        posY: 0,
        couleur: 'green',
        rayon: 2,
        temps: 3.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    { 
        id:6,
        typeName : "bubble",
        posX: 3,
        posY: 0,
        idTrajectoire: 1,
        couleur: 'magenta',
        rayon: 1,
        temps: 4,
        type: 1,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:8,
        typeName : "bubble",
        idTrajectoire: 1,
        posX: 6,
        posY: 0,
        couleur: 'grey',
        rayon: 1,
        temps: 4,
        type: 9,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "cibleToucherProlonger"
    },
    { 
        id:7,
        idBubble : 7,
        idCible : 8,
        typeName : "trajectory",
        posX: 5,
        posY: 0,
        couleur: 'blue',
        temps: 4,
        duration: 1.5, 
        width : 5,
        height : 3,
    },

    // 2 BULLES SIMPLES + PUZZLE
    { 
        id:9,
        typeName : "bubble",
        posX: 3,
        posY: 3,
        couleur: 'red',
        rayon: 2,
        temps: 4.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    { 
        id:10,
        typeName : "bubble",
        posX: 0,
        posY: 0,
        couleur: 'magenta',
        rayon: 2,
        temps: 5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    {
        id: 11,
        typeName: "bubble",
        posX: -4.5,
        posY: -3,
        couleur: 'white',
        rayon: 2,
        temps: 5.5,
        type: 7,
        duration: 1.5,
        rotation: 90,
        side: 1,
        texture: ""
    },
    {
        id: 12,
        typeName: "bubble",
        posX: 0.5,
        posY: -3,
        couleur: 'white',
        rayon: 2,
        temps: 5.5,
        type: 7,
        duration: 1.5,
        rotation: -90,
        side: 2,
        texture: ""
    },
    {
        id: 13,
        typeName: "bubble",
        posX: -2,
        posY: -3,
        couleur: 'white',
        rayon: 2,
        temps: 5.5,
        type: 6,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "ciblePuzzle"
    },

    // BULLE SIMPLE
    { 
        id:14,
        typeName : "bubble",
        posX: 3,
        posY: 3,
        couleur: 'yellow',
        rayon: 2,
        temps: 6.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    // DEUX TOUCHERS PROLONGERS
    { 
        id:15,
        typeName : "bubble",
        posX: -4.5,
        posY: 1.5,
        idTrajectoire: 1,
        couleur: 'red',
        rayon: 1,
        temps: 7,
        type: 1,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:17,
        typeName : "bubble",
        idTrajectoire: 1,
        posX: -1.5,
        posY: 1.5,
        couleur: 'grey',
        rayon: 1,
        temps: 7,
        type: 9,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "cibleToucherProlonger"
    },
    { 
        id:16,
        idBubble : 16,
        idCible : 17,
        typeName : "trajectory",
        posX: -3,
        posY: 1.5,
        couleur: 'green',
        temps: 7,
        duration: 1.5, 
        width : 5,
        height : 3,
    },

    { 
        id:18,
        typeName : "bubble",
        posX: 1.5,
        posY: -1.5,
        idTrajectoire: 1,
        couleur: 'red',
        rayon: 1,
        temps: 7,
        type: 1,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:20,
        typeName : "bubble",
        idTrajectoire: 1,
        posX: 4.5,
        posY: -1.5,
        couleur: 'grey',
        rayon: 1,
        temps: 7,
        type: 9,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "cibleToucherProlonger"
    },
    { 
        id:19,
        idBubble : 19,
        idCible : 20,
        typeName : "trajectory",
        posX: 3,
        posY: -1.5,
        couleur: 'white',
        temps: 7,
        duration: 1.5, 
        width : 5,
        height : 3,
    },

    // QUATRES BULLES SIMPLES
    { 
        id:21,
        typeName : "bubble",
        posX: -2.5,
        posY: -0.5,
        couleur: 'green',
        rayon: 2,
        temps: 8.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:22,
        typeName : "bubble",
        posX: -2.5,
        posY: 2.5,
        couleur: 'blue',
        rayon: 2,
        temps: 8.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:23,
        typeName : "bubble",
        posX: 2.5,
        posY: 2.5,
        couleur: 'green',
        rayon: 2,
        temps: 8.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:24,
        typeName : "bubble",
        posX: 2.5,
        posY: -0.5,
        couleur: 'blue',
        rayon: 2,
        temps: 8.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    // PUZZLE
    {
        id: 25,
        typeName: "bubble",
        posX: -2.5,
        posY: -3.5,
        couleur: 'white',
        rayon: 2,
        temps: 9,
        type: 7,
        duration: 1.5,
        rotation: 90,
        side: 1,
        texture: ""
    },
    {
        id: 26,
        typeName: "bubble",
        posX: 2.5,
        posY: -3.5,
        couleur: 'white',
        rayon: 2,
        temps: 9,
        type: 7,
        duration: 1.5,
        rotation: -90,
        side: 2,
        texture: ""
    },
    {
        id: 27,
        typeName: "bubble",
        posX: 0,
        posY: -3.5,
        couleur: 'yellow',
        rayon: 2,
        temps: 9,
        type: 6,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "ciblePuzzle"
    },

    // BULLES SIMPLES ALTERNEES
    { 
        id:28,
        typeName : "bubble",
        posX: -3.5,
        posY: 0,
        couleur: 'red',
        rayon: 2,
        temps: 10,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    { 
        id:29,
        typeName : "bubble",
        posX: 3.5,
        posY: 0,
        couleur: 'magenta',
        rayon: 2,
        temps: 10,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:30,
        typeName : "bubble",
        posX: -3.5,
        posY: 3.5,
        couleur: 'black',
        rayon: 2,
        temps: 10.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    { 
        id:31,
        typeName : "bubble",
        posX: 3.5,
        posY: -3.5,
        couleur: 'white',
        rayon: 2,
        temps: 10.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:32,
        typeName : "bubble",
        posX: -3.5,
        posY: -3.5,
        couleur: 'yellow',
        rayon: 2,
        temps: 11,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    { 
        id:33,
        typeName : "bubble",
        posX: 3.5,
        posY: 3.5,
        couleur: 'green',
        rayon: 2,
        temps: 11,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    // MALUS


    { 
        id:34,
        typeName : "bubble",
        type: 4,

        posX: 0,
        posY: 0,
        posXOpponent : 0,
        posYOpponent : 0,
        impulsion: 0.5,

        couleur: 'white',
        rayon: 2,
        temps: 12.5,
        duration: 0.5,
        rotation: 90,
        side: 0,
        freezeDuration : 7,
        texture: "flocon"
    },




    // DEUX TOUCHERS PROLONGERS

    { 
        id:35,
        typeName : "bubble",
        posX: -3.5,
        posY: -2,
        idTrajectoire: 1,
        couleur: 'magenta',
        rayon: 1,
        temps: 14.5,
        type: 1,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:37,
        typeName : "bubble",
        idTrajectoire: 1,
        posX: -1,
        posY: -2,
        couleur: 'grey',
        rayon: 1,
        temps: 14.5,
        type: 9,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "cibleToucherProlonger"
    },
    { 
        id:36,
        idBubble : 36,
        idCible : 37,
        typeName : "trajectory",
        posX: -2,
        posY: -2,
        couleur: 'blue',
        temps: 14.5,
        duration: 1.5, 
        width : 5,
        height : 3,
    },

    { 
        id:38,
        typeName : "bubble",
        posX: 0.5,
        posY: 2,
        idTrajectoire: 1,
        couleur: 'red',
        rayon: 1,
        temps: 14.5,
        type: 1,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    { 
        id:40,
        typeName : "bubble",
        idTrajectoire: 1,
        posX: 3,
        posY: 2,
        couleur: 'grey',
        rayon: 1,
        temps: 14.5,
        type: 9,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "cibleToucherProlonger"
    },
    { 
        id:39,
        idBubble : 39,
        idCible : 40,
        typeName : "trajectory",
        posX: 2,
        posY: 2,
        couleur: 'white',
        temps: 14.5,
        duration: 1.5, 
        width : 5,
        height : 3,
    },


    // TROIS TOUCHERS SIMPLES + PUZZLE 
    { 
        id:40,
        typeName : "bubble",
        posX: 0,
        posY: 2.75,
        couleur: 'yellow',
        rayon: 2,
        temps: 16,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },

    { 
        id:41,
        typeName : "bubble",
        posX: 0,
        posY: 0,
        couleur: 'red',
        rayon: 2,
        temps: 16.5,
        type: 0,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "bubble2"
    },
    {
        id: 42,
        typeName: "bubble",
        posX: -2.5,
        posY: -3,
        couleur: 'white',
        rayon: 2,
        temps: 17.25,
        type: 7,
        duration: 1.5,
        rotation: 90,
        side: 1,
        texture: ""
    },
    {
        id: 43,
        typeName: "bubble",
        posX: 2.5,
        posY: -3,
        couleur: 'white',
        rayon: 2,
        temps: 17.25,
        type: 7,
        duration: 1.5,
        rotation: -90,
        side: 2,
        texture: ""
    },
    {
        id: 44,
        typeName: "bubble",
        posX: 0,
        posY: -3,
        couleur: 'white',
        rayon: 2,
        temps: 17.25,
        type: 6,
        duration: 1.5,
        rotation: 90,
        side: 0,
        texture: "ciblePuzzle"
    },
   
]