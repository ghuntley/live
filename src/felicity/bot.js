const Discord = require('discord.js');
const botsettings = require('./botsettings.json');
const bot = new Discord.Client();
const fs = require('fs');
bot.commands = new Discord.Collection();

fs.readdir("./commands/", (err, files) => {

    if (err) console.log(err);

    let jsfile = files.filter(f => f.split(".").pop() === "js");

    if (jsfile.length <= 0) {
        console.log("Couldn't find commands.");
        return;
    }

    jsfile.forEach((f, i) => {

        let props = require(`./commands/${f}`);
        console.log(`${f} was loaded successfully.`);
        bot.commands.set(props.help.name, props);
    });
});

bot.on("guildMemberAdd", async (guildMember) => {
    var role = guildMember.guild.roles.find('name', 'Guest');

    guildMember.addRole(role);

    guildMember.send('`Hey there! Thanks for joining our server. We hope you have a fun time! Invite your friends!`');
    
});

bot.on("ready", async () => {
    console.log(`${bot.user.username} is ready to go.`);
    bot.user.setActivity("with !help for help", { type:"PLAYING" }); 
});

bot.on("message", async (message) => {
    
    if (message.author.bot) return;
    if (message.channel.type === "dm") return;

    let messageArray = message.content.split(" ");
    let cmd = messageArray[0];
    let args = messageArray.slice(1);

    let commandfile = bot.commands.get(cmd.slice(botsettings.prefix.length));
    if (commandfile) commandfile.run(bot, message, args);
});


bot.on('messageDelete', async (message) => {
      
      let user = ""
        if (entry.extra.channel.id === message.channel.id
          && (entry.target.id === message.author.id)
          && (entry.createdTimestamp > (Date.now() - 5000))
          && (entry.extra.count >= 1)) {
        user = entry.executor.username
      } else { 
        user = message.author.username
      }
      message.channel.send(`A message was deleted in ${message.channel.name} by ${user}`);
});

bot.login(botsettings.token);
