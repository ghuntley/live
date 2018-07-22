const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {

    message.delete();

    if (!message.member.hasPermission("ADMINISTRATOR")) return message.channel.send("You do not have the correct permissions to use this command.");
    let announceChannel = message.guild.channels.find("name", "announcements");
    let announcement = args.join(" ");

    announceChannel.send(`@everyone, ${announcement}`);

}

module.exports.help = {
    name: "announce"
}