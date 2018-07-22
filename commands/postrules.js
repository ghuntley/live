const Discord = require('discord.js');

module.exports.run = async (bot, message, args) => {

    message.delete();

    if (!message.member.hasPermission("ADMINISTRATOR")) return message.channel.send("You do not have the correct permissions to use this command.");
    let rulesChannel = message.guild.channels.find("name", "rules");

    rulesChannel.send("```md\n1. No Racism / Sexism / Overall abuse towards other players.\n2. No self-advertising unless given permission by Geoffrey.\n3. Do not spam bot commands in anywhere other than #bot-spam. It is there for a reason.\n4. Do not spam #programming-help with non-programming related questions.```")
    


}

module.exports.help = {
    name: "postrules"
}