function delay(time) {
    return new Promise(resolve => setTimeout(resolve, time));
}

module.exports = async function wait(time) {
    await delay(time);
}