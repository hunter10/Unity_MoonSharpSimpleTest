LuaTest3 = {}

function LuaTest3:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o 
end


function LuaTest3:OnClickDoLocalMove()
  
  	--[[
    print("LuaTest2:OnclickDoLocalMove end")

    self.go = UnityEngine.GameObject.Find("Image-LuaTest")
    --self.go.transform.localposition = UnityEngine.Vector3.__new(0.1, 0, 0)
    self.go.transform.localPosition = UnityEngine.Vector3.__new(15, 15, 0)
        
    print("LuaTest2:OnclickDoLocalMove end")
	]]
end