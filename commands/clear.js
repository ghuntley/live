const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {

    message.delete();

    if (!message.member.hasPermission("MANAGE_MESSAGES")) return message.channel.send("You do not have permission to use this command.");

    let deleteAmount = args[0];

    if (!deleteAmount) return message.channel.send("Please enter a valid amount of messages to delete.");

    message.channel.bulkDelete(deleteAmount).then(() => {
        message.channel.send(`Deleted ${deleteAmount} message(s).`).then(msg => msg.delete(7500));
    });


}

module.exports.help = {
    name: "clear"
}