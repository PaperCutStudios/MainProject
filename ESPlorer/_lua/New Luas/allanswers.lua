local allanswered = ...
return function()
	local players = require("prefs").players
	local answers={}
	for key,value in pairs(players) do
		table.insert(players, require("players"..value).id)
	end
	for key,value in pairs(players) do
		local player = require("player"..value)
		local result = 0
		for k,v in pairs(answers) do
			if(player.answer:sub(1,1) ==v:sub(1,1) and player.answer:sub(2,2) == v:sub(2,2) and player.answer:sub(3) == v:sub(3)) then
				result = result +1
			end
			player.socket:send("8"..numresult)
		end
	end
end
