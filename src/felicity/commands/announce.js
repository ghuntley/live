const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {

    message.delete();

    if (!message.member.hasPermission("ADMINISTRATOR")) return message.channel.send("You do not have the correct permissions to use this command.").then(msg => msg.delete(7500));
    let announceChannel = message.guild.channels.find("name", "announcements");
    if (!announceChannel) return message.channel.send("There is no #announcements channel.");
    let announcement = args.join(" ");

    announceChannel.send(`${announcement}`);

}

module.exports.help = {
    name: "announce"
}