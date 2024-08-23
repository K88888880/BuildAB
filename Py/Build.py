
# import  subprocess
# import  shutil
# import  os
# from http.cookiejar import debug
#
# UNITYPATH="E:\\unityAnZhuang\\2021.3.34f1c1\\Editor\\Unity.exe"
# PROJECTPATH="E:\\unityXm\\My project (ABBuild)"
# platform = "Android"
# outpath="E:\\unityXm\\My project (ABBuild)\\BuildTarget\\"
# age="fds"
#
# print("begin test")
# #subprocess.call(UNITYPATH+"-quit"+"-batchmode"+"-projectPath"+PROJECTPATH+"-executeMethod ABBuild.Build"+platform)
# subprocess.call(UNITYPATH + " -quit " + " -batchmode " + " -projectpath " + PROJECTPATH + " -executeMethod ABBuild.Bu " +"fd"+"fds" + platform+outpath+age)
#
# print("end test")

import subprocess
import shutil
import os

UNITYPATH = "E:\\unityAnZhuang\\2021.3.34f1c1\\Editor\\Unity.exe"

MAPPROJECT = "E:\\unityXm\\My project (ABBuild)"
companyName = "com"
productName = "PersernolTax"
platform = "0"
outPath = "E:\\unityXm\\My project (ABBuild)\\BuildTarget\\Android"
arg0 = "1111"
icon = "E:\\1\\icon.png"
assetresource = "E:\\1\\game\\Assets\\Resources"
font = "E:\\data\\font\\simkai.ttf"

command = f'{UNITYPATH} -quit -batchmode -projectPath "{MAPPROJECT}" -executeMethod ABBuild.Bu "{companyName}" "{productName}" "{platform}" "{outPath}" "{arg0}"'
subprocess.call(command, shell=True)

print("build package done")