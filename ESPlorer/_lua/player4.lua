local player4 = {}
player4.ID = 4;
player4.IP = "";
player4.AnswerIDs = "";
function player4.SetID(id)
    player4.ID = id;
end
function player4.SetIP(ip)
    player4.IP = ip;
end
function player4.SetAnswer(answerid)
    player4.AnswerIDs = answerid;
end
return player4;
