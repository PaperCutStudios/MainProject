local gamePrefs = {}
gamePrefs.Difficulty = 1
gamePrefs.Time = 270
gamePrefs.Players = {}
gamePrefs.Seed = 0
gamePrefs.AnswersEntered = 0
gamePrefs.GameStarted = false
function gamePrefs.GetSeed()
    if (gamePrefs.Seed == nil or gamePrefs.Seed == 0) then
        gamePrefs.Seed = math.random(999999)
        print("Getting Seed: "..gamePrefs.Seed)
        return gamePrefs.Seed
    else
        return gamePrefs.Seed
    end
end
function gamePrefs.GetAvailablePlayer()
    --If there are no players currently added to the game, return player 1 as available
    if(table.getn(gamePrefs.Players) == 0) then
        return 1
    else
       --check through the players table for the next available player number (1-4)
        for i=1, 4 do
            local found = false
            for key,value in pairs(gamePrefs.Players) do
                if(value == i) then
                    found = true
                    break
                end
            end    
            --if we havent found any of num 'i' in the table, this function will return
            if(found == false) then
                return i
            end
        end
        return 0
    end
end
function gamePrefs.RemovePlayer(playernum)
    local toberemoved = 0
    for key,value in pairs(gamePrefs.Players) do
        if(value == playernum) then
            toberemoved = key
            break
        end
    end
    table.remove(gamePrefs.Players, toberemoved)
end
function gamePrefs.AnswerAdded()
print("Answer Added")
    gamePrefs.AnswersEntered = gamePrefs.AnswersEntered +1
    if (gamePrefs.AnswersEntered == table.getn(gamePrefs.Players) ) then
    print("TOTAL ANSWERS RECEIVED")
        --create a table of each of the connected player's answers
        local playeranswers = {}
        for key,value in pairs(gamePrefs.Players) do 
            player = require("player"..value)
            table.insert(playeranswers, player.AnswerIDs)
        end
        for pkey,pvalue in pairs(gamePrefs.Players) do
            local player = require("player"..value)
            local numresult = 0
            for akey,avalue in pairs(playeranswers) do
                if(player.AnswerIDs:sub(1,1) == avalue:sub(1,1) and player.AnswerIDs:sub(2,2) == avalue:sub(2,2) and player.AnswerIDs:sub(3) == avalue:sub(3)) then
                    numresult = numresult + 1
                end
            end
            player.Socket:send("8"..numresult)
        end
    end
end
return gamePrefs
