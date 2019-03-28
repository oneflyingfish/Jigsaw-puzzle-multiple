# Jigsaw-puzzle-multiple
基于C#开发的智力拼图游戏

## 开发说明：  
开发环境：`Visual Stdio 2017 + Windows 10 + .net framework 4.5.2`  
开发语言：`C#`  

## 实现原理：
* Tag: 每块PictureBox的附加数字信息，若n = 3，则初始状态为：   
       [ 0 1 2 ]  
       [ 3 4 5 ]  
       [ 6 7 8 ]  
* 空白块：设置PictureBox的Visible = false
* 图片交换：交换PictureBox的Image、Visible、以及Tag属性
* 混淆矩阵：将空白块随机与周围的块替换
* 判决是否正确：根据tag是否均回归到初始值

## 特色说明：
* 能随意设置矩阵规模（游戏难度），支持 `3*3 ~ 13*13`
* 能定制图片
