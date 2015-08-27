local player = {}
player.ID = 2;
player.AnswerIDs = " ";
player.Socket = ""
function player.connect(Socket)
    player.Socket = Socket;
    print("Player "..player.ID.." has Joined")
    require("lights").on("green")
end
function player.disconnect()
    print("Player "..player.ID.." has Disconnected")
    require("lights").off("green")
end
function player.setAnswer(answer)
    print("Setting Player "..player.ID.."'s Answer to: ".. answer)
    player.AnswerIDs = answer
    require("gamePrefs").AnswerAdded()
end
return player;
