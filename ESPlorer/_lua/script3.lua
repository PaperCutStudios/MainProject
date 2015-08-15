function dicks()
local player = require("player1")
print (player.AnswerIDs)
player.AnswerIDs = "Hello!"
print (player.AnswerIDs)
end
function dicks2()
local player = require("player1")
print (player.AnswerIDs)
end

dicks()
dicks2()
