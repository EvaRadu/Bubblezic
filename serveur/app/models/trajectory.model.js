const Joi  = require('joi');
const { Balle } = require('.');

/**
 * The Trajectory model
 */
module.exports = Joi.object().keys({
    Id: Joi.number().required,
    idBubble : Joi.number().required,
    idCible: Joi.number().required,
    posX: Joi.number().required,
    posY: Joi.number().required,
    couleur: Joi.string(),
    temps: Joi.number(), // a quel moment le path apparait
    duration: Joi.number(),
    width : Joy.number().required,
    height : Joy.number().required,
})