local player = {}
player.ID = 4;
player.AnswerIDs = " ";
player.Socket = ""
function player.connect(Socket)
    player.Socket = Socket;
    print("Player "..player.ID.." has Joined")
    require("lights").LightOn("red")
end
function player.disconnect()
    print("Player "..player.ID.." has Disconnected")
    require("lights").LightOff("red")
end
function player.setAnswer(answer)
    print("Setting Player "..player.ID.."'s Answer to: ".. answer)
    player.AnswerIDs = answer
    require("gamePrefs").AnswerAdded()
end
return player;
