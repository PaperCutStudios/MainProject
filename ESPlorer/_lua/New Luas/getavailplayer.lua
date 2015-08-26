local gap = {}
function gap.getavailplayer()
	print("test1")
	local prefs = require("prefs")
	if(table.getn(prefs.players)==0)then
		print("test2")
		table.insert(prefs.players, 1)
		return 1
	else
		print("test3")
		for i=1,4 do
			local found = false
			for key,value in pairs(prefs.players) do
				if(value == i) then
					found = true
					break
				end
			end
			if(found == false) then 
				table.insert(prefs.players, i)
				return i
			end
		end
		return 0
	end
end
return gap
