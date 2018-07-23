const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {
    
    let embed = new Discord.RichEmbed()
        .setColor("#ea3a43")
        .addField("Administrator Commands", "!announce  !clear  !kick  !ban  !postrules")
        .addField("Community Commands", "!serverinfo  !schedule  !userinfo  !help  !botinfo  !uptime");

    message.channel.send(embed);
}

module.exports.help = {
    name: "help"
}