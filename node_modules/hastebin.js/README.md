# hastebin.js

A NPM package to post data to hastebin.

## Example Usage

```js
const hastebin = require('hastebin.js');

const haste = new hastebin();

const link = haste.post('Helllo from hastebin.js!');
console.log(link);
// Will return a link such as https://hastebin.com/sofomuqifo.js
```