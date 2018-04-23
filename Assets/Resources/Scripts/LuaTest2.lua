LuaTest2 = {}

function LuaTest2:new(o)
    o = o or {}
    setmetatable(o, self)
    self.__index = self
    return o 
end


function LuaTest2:OnClickDoLocalMove()
  
    print("LuaTest2:OnclickDoLocalMove end")

    self.go = UnityEngine.GameObject.Find("Image-LuaTest")
    self.go.transform.position = UnityEngine.Vector3.__new(-1, 0, 0)
    
    print("LuaTest2:OnclickDoLocalMove end")

end