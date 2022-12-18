const Joi  = require('joi');
const { Trajectory } = require('.');

/**
 * The Balle model
 */
module.exports = Joi.object().keys({
    typeName : Joi.string().required,
    Id: Joi.number().required,
    idTrajectoire: Joi.number(),
    posX: Joi.number().required,
    posY: Joi.number().required,
    couleur: Joi.string(),
    rayon: Joi.number(),
    temps: Joi.number(), // a quel moment la balle apparait
    type: Joi.number(),          // différent type de balles, voir, l'objet type
    duration: Joi.number(),
})