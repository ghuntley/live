const { post, get } = require('snekfetch');

module.exports = class Hastebin {
  constructor() {

  }

  async post(code) {
    const { body } = await post('https://hastebin.com/documents').send(code).catch(console.error);
    const url = `https://hastebin.com/${body.key}.js`;
    return url;
  }

  async get(key) {
    const url = `https://hastebin.com/${key}.js`;
    const body = await get(url);
    if (body.status === 200) {
      return url;
    } else {
      throw new Error('That file does not exist. Please check your spelling and try again.');
    }
  }
};