const Joi  = require('joi');
const Type = require('./type.model.js');

/**
 * The Balle model
 */
module.exports = Joi.object().keys({
    Id: Joi.number().required,
    posX: Joi.number().required,
    posY: Joi.number().required,
    couleur: Joi.string(),
    rayon: Joi.number(),
    temps: Joi.number(),
    type: Type
})