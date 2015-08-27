function HandleSocketData (Socket,DataStream)
print("Receiving data: "..DataStream)
    if(DataStream:sub(1,1) == "0") then
        local gamePrefs = require("gamePrefs")
        local playernum = gamePrefs.GetAvailablePlayer()
        if(playernum == 0) then
            SocketPass:send("0")
        else
            print("Attemping to give player number")
            local playerfile = "player"..playernum
            local player = require(playerfile)
            player.connect(Socket)
            playerfile = "1".. player.ID .. gamePrefs.Difficulty .. gamePrefs.Time
            player.Socket:send(playerfile)
            print(playerfile)
            table.insert(gamePrefs.Players, player.ID)
        end
    elseif(tonumber(DataStream:sub(1,1)) <= 4) then
        local player = require("player"..DataStream:sub(1,1))
        print("DataStream[2] = "..DataStream:sub(2,2))
        if(DataStream:sub(2,2) == "0") then
            print ("disconnecting player"..player.ID)
            player.disconnect()
            local gamePrefs = require("gamePrefs")
            gamePrefs.RemovePlayer(player.ID)
        elseif(DataStream:sub(2,2) == "1") then
            print ("Player "..player.ID.." requesting game seed")
            local gamePrefs = require("gamePrefs")
            local seed = gamePrefs.GetSeed()
            player.Socket:send("2"..string.len(seed)..seed)
            print("Sent Seed: ".."2"..string.len(seed)..seed)
        elseif((DataStream:sub(2,2) == "2")) then
            print ("Player "..player.ID.." sending their answer")
            player.setAnswer(DataStream:sub(3))          
        end            
    else
        print("Data not workable")       
    end
end
function OnConnection(Socket)
    Socket:on("receive", HandleSocketData)
    Socket:on("disconnection", function(sck, c) print("Disconnect") end)
end

local buttons = require("ButtonFuncs")
buttons.SetButtonTrigs()
Server:listen(80,OnConnection)
require("lights").off("blue")
