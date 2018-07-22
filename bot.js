const Discord = require('discord.js');
const botsettings = require('./botsettings.json');
const bot = new Discord.Client();
const fs = require('fs');
bot.commands = new Discord.Collection();

fs.readdir("./commands/", (err, files) => {

    if (err) console.log(err);

    let jsfile = files.filter(f => f.split(".").pop() === "js");

    if (jsfile.length <= 0) {
        console.log("Couldn;t find commands.");
        return;
    }

    jsfile.forEach((f, i) => {

        let props = require(`./commands/${f}`);
        console.log(`${f} was loaded successfully.`);
        bot.commands.set(props.help.name, props);
    });
});

bot.on("ready", async () => {
    console.log(`${bot.user.username} is ready to go.`);
    bot.user.setActivity("with Geoffrey", { type:"PLAYING" }); 
});

bot.on("message", async (message) => {
    
    if(message.author.bot) return;
    if(message.channel.type === "dm") return;

    let messageArray = message.content.split(" ");
    let cmd = messageArray[0];
    let args = messageArray.slice(1);

    let commandfile = bot.commands.get(cmd.slice(botsettings.prefix.length));
    if(commandfile) commandfile.run(bot, message, args);

    
});

bot.login(botsettings.token);