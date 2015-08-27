local gap = {}
function gap.getavailplayer()
	print("test1")
	local prefs = require("prefs")
	print("test3")
	for i=1,4 do
        print(4,i)
		local found = false
		for key,value in pairs(prefs.players) do
			if(value == i) then
				found = true
				break
			end
		end
		if(found == false) then 
			table.insert(prefs.players, i)
            print(i)
			return i
		end
	end
	return 0
end
return gap
