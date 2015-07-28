local player2 = {}
player2.ID = 2;
player2.IP = "";
player2.AnswerIDs = "";
function player2.SetID(id)
    player2.ID = id;
end
function player2.SetIP(ip)
    player2.IP = ip;
end
function player2.SetAnswer(answerid)
    player2.AnswerIDs = answerid;
end
return player2;
