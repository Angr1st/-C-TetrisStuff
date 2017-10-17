#SingleInstance, Force

Gui, Font,, Modern No. 20 
Gui, Add, Button, w180 gButtonGO, GOOOOO!!!!
Gui, Add, Button, w180 gButtonHighscores, Highscores
Gui, Add, Button, w180 gButtonOptions, Options
Gui, Add, Button, w180 gButtonExit, Exit
Gui, show, w200 h120, Menu / Tetris
return 

Score = 12

ButtonGO:
FileRead, %UserName%, C:\Users\%UserName%\Desktop\Tetris\TetrisUserName.txt
Hoehe = 20
Breite = 10
Feld := Object()
Feldtmp := Object()
XPos = -20
YPos = -20
speed = 20
TCounter = 0
Score = 000
Gui, Submit, NoHide
Gui, New
Gui, Font,, Modern No. 20 
Gui, Add, Text,x265, Name (Kuerzel)
Gui, Add, Edit,Limit2 x265 w75 r1 vName
Gui, Add, Button, w75, Save
Gui, Add, Text,, Score
Gui, Add, Text,, %Score%
Gui, Add, Text,, Next Block
Gui, Add, Button, x265 y370 w75 gButtonRestart, Restart
Gui, Margin, 0,0
Gui, Add, Picture, x0 y0 w200 h400 vBill , Schwarz.png
Gui, Show, w350 h400, Tetris

HCounter = 0
BCounter = 0
Loop , %Hoehe%
{
	BCounter = 0
	Loop, %Breite%
	{
		Gui,Add,Picture, w20 h20 vTed%TCounter%, schwarz.png
		Guicontrol, move, Ted%TCounter%, % "X" 20*BCounter "Y" 20*HCounter
		tmpx := 210+10*BCounter
		tmpy := 20*HCounter
		GuiControl, move, Feld2%HCounter%%BCounter%, x%tmpX% y%tmpy%
		Feld[HCounter, BCounter] := 0
		BCounter++
		TCounter++
	}
	HCounter++
}
	gui, add, picture, x20 y20 w20 h20 vTest0, rot.png
	guicontrol, move, test0, % "x" -20 "y" -20
	gui, add, picture, x20 y40 w20 h20 vTest1, rot.png
	guicontrol, move, test1, % "x" -20 "y" -20
	gui, add, picture, x20 y60 w20 h20 vTest2, rot.png
	guicontrol, move, test2, % "x" -20 "y" -20
	gui, add, picture, x20 y80 w20 h20 vTest3, rot.png
	guicontrol, move, test3, % "x" -20 "y" -20
	
	gui, add, picture, x20 y20 w20 h20 vNext0, rot.png
	guicontrol, move, next0, % "x" -20 "y" -20
	gui, add, picture, x20 y40 w20 h20 vNext1, rot.png
	guicontrol, move, next1, % "x" -20 "y" -20
	gui, add, picture, x20 y60 w20 h20 vNext2, rot.png
	guicontrol, move, next2, % "x" -20 "y" -20
	gui, add, picture, x20 y80 w20 h20 vNext3, rot.png
	guicontrol, move, next3, % "x" -20 "y" -20


	; text00 := 9
	; text01 := 9
	; text02 := 9
	; text03 := 9

	; text10 := 9
	; text11 := 9
	; text12 := 9
	; text13 := 9

	; text20 := 9
	; text21 := 9
	; text22 := 9
	; text23 := 9

	; text30 := 9
	; text31 := 9
	; text32 := 9
	; text33 := 9
	

	; Gui,  add, text, vtext00 w20 ys section, %text00%
	; Gui,  add, text, vtext01 w20, %text01%
	; Gui,  add, text, vtext02 w20, %text02%
	; Gui,  add, text, vtext03 w20, %text03%

	; Gui,  add, text, vtext10 w20 ys section, %text10%
	; Gui,  add, text, vtext11 w20, %text11%
	; Gui,  add, text, vtext12 w20, %text12%
	; Gui,  add, text, vtext13 w20, %text13%

	; Gui,  add, text, vtext20 w20 ys section, %text20%
	; Gui,  add, text, vtext21 w20, %text21%
	; Gui,  add, text, vtext22 w20, %text22%
	; Gui,  add, text, vtext23 w20, %text23%

	; Gui,  add, text, vtext30 w20 ys, %text30%
	; Gui,  add, text, vtext31 w20, %text31%
	; Gui,  add, text, vtext32 w20, %text32%
	; Gui,  add, text, vtext33 w20, %text33%

	
running := 0
left := 0
leftOnce := 0
right := 0
rightOnce :=0
down := 0
downOnce := 0
aKey := 0
aOnce := 0
sKey := 0
sOnce := 0
currentBlockColor := 0
nextBlockColor := 0

count := 4
currentBlock := Object()
nextBlock := Object()
bufferBlock := Object()

gosub createShape
gosub createShape

while(running = 0)
{
	sleep 10
	gosub logik
	gosub input
	gosub draw
}
return

logik:
	if(left = 1)
	{
		gosub moveLeft
	}
	else
	{
		leftOnce := 0
	}
	
	if(right = 1)
	{
		gosub moveRight
	}
	else
	{
		rightOnce := 0
	}
	
	if(down = 1)
	{
		gosub moveDown
	}
	else
	{
		downOnce := 0
	}
	
	if(aKey = 1)
	{
		gosub spinLeft
	}
	else
	{
		aOnce := 0
	}
	
	if(sKey = 1)
	{
		gosub spinRight
	}
	else
	{
		sOnce := 0
	}
	
	gosub blockSenken
return

Reihenabgleich:
	HCounter = 0
	BCounter = 0
	PCounter = 0
	RCounter = 0
	RFull = 0
	DCounter = 0
	Multi = 0
	RLeer%Hoehe% := 0
	Loop , %Hoehe%
	{
		Loop , %Breite%
		{
			tmp := Feld[HCounter, BCounter]
			; msgbox, feld %tmp% H %HCounter% B %BCounter%
			If (Feld[HCounter, BCounter] = 0)
			{
				RCounter++
				if (RCounter == Breite)
				{
					RLeer%HCounter% := 1
				}
			}
			else
			{
				PCounter++
				RLeer%HCounter% := 0
				if (PCounter == Breite)
				{
					Multi++
					RFull++
					DCounter= 0
					loop , %Breite%
					{
						Feld[HCounter, DCounter] := 0
						DCounter++
					}
					Delet:=1
					DCounter= 0
					RLeer%HCounter% := 1
				}
			}
			BCounter++
		}
		HCounter++
		PCounter = 0
		RCounter = 0
		BCounter = 0
	}
	; if (Delet == 1)
	; {
		; gosub updatefield
		; Delet:= 0
	; }	
	Score := (RFull*100*Multi) + Score
	Gui, Submit, NoHide
	ACounter := 0
	HCounter := Hoehe
	BCounter := 0
	ZwiSP := Hoehe
	
	Loop, %Hoehe%
	{
		BCounter := 0
		ZwiSP := Hoehe
		found := 0
		loop, %Hoehe%
		{
			if(RLeer%ZwiSP% = 0)
			{
				RLeer%ZwiSP% := 1
				updateRows%ZwiSP% := 1
				break
			}
			ZwiSP--
		}
			loop, %Breite%
			{
				Feldtmp[HCounter, BCounter] := Feld[ZwiSP, BCounter]
				BCounter++
			}
			Feldtmp[HCounter, BCounter] := 0
		HCounter--
	}
	
	m := 0
	loop, %Hoehe%
	{
		l := 0
		loop, %Breite%
		{
			; tmp = Feldtmp[m,l]
			; if(tmp = 1 || tmp = 2 || tmp = 3 || tmp = 4 || tmp = 5 || tmp = 6 || tmp = 7 || tmp = 0)
			; {
			
			; }
			; else
			; {
				; Feldtmp[m,l] := 0
			; }
				if(updateRows%m% = 1)
				{
					tednr := ((10*(m))+l)
					;msgbox, %tednr%
					Feld[m,l] := Feldtmp[m,l]
					if (Feldtmp[m, l] == 0 )
					{
						GuiControl,,Ted%tednr%, *w20 *h20 Schwarz.png
					}
					if (Feldtmp[m, l] == 1 )
					{
						GuiControl,,Ted%tednr%, *w20 *h20 Rot.png
					}
					if (Feldtmp[m, l] == 2 )
					{
						GuiControl,,Ted%tednr%, *w20 *h20 Blau.png
					}
					if (Feldtmp[m, l] == 3 )
					{
						GuiControl,,Ted%tednr%, *w20 *h20 Grün.png
					}
					if (Feldtmp[m, l] == 4 )
					{
						GuiControl,,Ted%tednr%, *w20 *h20 Gelb.png
					}
					if (Feldtmp[m, l] == 5 )
					{
						GuiControl,,Ted%tednr%, *w20 *h20 Orange.png	
					}
					if (Feldtmp[m, l] == 6 )
					{
						GuiControl,,Ted%tednr%, *w20 *h20 Lila.png
					}
					if (Feldtmp[m, l] == 7 )
					{
						GuiControl,,Ted%tednr%, *w20 *h20 Türkis.png
					}
				}
			l++
		}
		m++
	}
	; Loop , %Hoehe%
	; {
		; Loop , %Hoehe%
		; {
			; if (RLeer%HCounter% == 1 & HCounter != 0)
			; {
				; ACounter := HCounter - 1
				; loop , %Breite%
				; {
					; ZwiSp := Feld[ACounter, BCounter]
					; Feld[HCounter, BCounter] := ZwiSp
					; BCounter++
				; }
				; RLeer%HCounter% := 0
				; RLeer%ACounter% := 1
				; BCounter := 0
			; }
			; HCounter++
		; }
		; HCounter := 0
	; }
return

; Reihenabgleich:
	; HCounter = 0
	; BCounter = 0
	; PCounter = 0
	; RCounter = 0
	; RFull = 0
	; DCounter = 0
	; Multi = 0
	; RLeer%Hoehe% := 0
	; Loop , %Hoehe%
	; {
		; Loop , %Breite%
		; {
			; tmp := Feld[HCounter, BCounter]
			 ; msgbox, feld %tmp% H %HCounter% B %BCounter%
			; If (Feld[HCounter, BCounter] = 0)
			; {
				; RCounter++
			; }
			; else
			; {
				; PCounter++
				; RLeer%HCounter% := 0
			; }
			; BCounter++
			; if (PCounter = 9)
			; {
				; ACounter := HCounter
				; Multi++
				; RFull++
				; DCounter= 0
				; loop , %Breite%
				; {
					; Feld[HCounter, DCounter] := 0
					; DCounter++
				; }
				; DCounter= 0
				; RLeer%ACounter% := 1
			; }
			; if (RCounter == Breite)
			; {
				; ACounter := HCounter
				; RLeer%ACounter% := 1
			; }
		; }
		; HCounter++
		; PCounter = 0
		; RCounter = 0
		; BCounter = 0
	; }
	; Score := (RFull*100*Multi) + Score
	; ACounter := 0
	; HCounter := 0
	; BCounter := 0
	; CCounter := 1
	; ZwiSP := 0
	; Loop , %Hoehe%
	; {
		; if (RLeer%HCounter% == 1 && HCounter != 0)
		; {
			; ACounter := HCounter - 1
			; loop , %Breite%
			; {
				; ZwiSp := Feld[ACounter, BCounter]
				; Feld[HCounter, BCounter] := ZwiSp
				; BCounter++
			; }
			; RLeer%HCounter% := 0
			; RLeer%ACounter% := 1
			; BCounter := 0
		; }
		; HCounter++
	; }
	; HCounter := 0
; return

draw:

; zeichnet nächsten Block
	i = 0
	loop, 4
	{
		j = 0
		loop, 4
		{
			if(nextBlock[i, j] != 9)
			{
				if(nextBlock[i, j] = 0)
				{
					guicontrol, movedraw, next0, % "x" 20*i+250 "y" 20*j+225
				}
				if(nextBlock[i, j] = 1)
				{
					guicontrol, movedraw, next1, % "x" 20*i+250 "y" 20*j+225
				}
				if(nextBlock[i, j] = 2)
				{
					guicontrol, movedraw, next2, % "x" 20*i+250 "y" 20*j+225
				}
				if(nextBlock[i, j] = 3)
				{
					guicontrol, movedraw, next3, % "x" 20*i+250 "y" 20*j+225
				}
			}
			j++
		}
		i++
	}
		; zeichnet momentan aktiven block
	
	i = 0
	loop, 4
	{
		j = 0
		loop, 4
		{
			if(currentBlock[i, j] != 9)
			{
				if(currentBlock[i, j] = 0)
				{
					guicontrol, movedraw, Test0, % "x" 20*i+20*currentBlockX "y" 20*j-80+20*currentBlockY
				}
				if(currentBlock[i, j] = 1)
				{
					guicontrol, movedraw, Test1, % "x" 20*i+20*currentBlockX "y" 20*j-80+20*currentBlockY
				}
				if(currentBlock[i, j] = 2)
				{
					guicontrol, movedraw, Test2, % "x" 20*i+20*currentBlockX "y" 20*j-80+20*currentBlockY
				}
				if(currentBlock[i, j] = 3)
				{
					guicontrol, movedraw, Test3, % "x" 20*i+20*currentBlockX "y" 20*j-80+20*currentBlockY
				}
			}
			j++
		}
		i++
	}
return
	
input:
	left := GetKeyState("Left")
	right := GetKeyState("Right")
	down := GetKeyState("Down")
	aKey := GetKeyState("a")
	sKey := GetKeyState("s")
return

updateField:
	HCounter = 0
	TCounter = 0
	Loop , %Hoehe%
	{
		Bcounter = 0
		Loop , %Breite%
		{
			if (Feld[HCounter, BCounter] == 0 )
			{
				GuiControl,,Ted%TCounter%, *w20 *h20 Schwarz.png
			}
			if (Feld[HCounter, BCounter] == 1 )
			{
				GuiControl,,Ted%TCounter%, *w20 *h20 Rot.png
			}
			if (Feld[HCounter, BCounter] == 2 )
			{
				GuiControl,,Ted%TCounter%, *w20 *h20 Blau.png
			}
			if (Feld[HCounter, BCounter] == 3 )
			{
				GuiControl,,Ted%TCounter%, *w20 *h20 Grün.png
			}
			if (Feld[HCounter, BCounter] == 4 )
			{
				GuiControl,,Ted%TCounter%, *w20 *h20 Gelb.png
			}
			if (Feld[HCounter, BCounter] == 5 )
			{
				GuiControl,,Ted%TCounter%, *w20 *h20 Orange.png	
			}
			if (Feld[HCounter, BCounter] == 6 )
			{
				GuiControl,,Ted%TCounter%, *w20 *h20 Lila.png
			}
			if (Feld[HCounter, BCounter] == 7 )
			{
				GuiControl,,Ted%TCounter%, *w20 *h20 Türkis.png
			}
			BCounter++
			TCounter++
		}
		HCounter++
	}
return

blockSenken:
	k++
	if(k == speed)
	{
		l := 0
		loop, 4
		{
			m := 0
			loop, 4
			{
				if(currentBlock[m, l] != 9)
				{
					if(currentBlockY >= 20)
					{
						if(currentBlock[0,3] != 9 || currentBlock[1,3] != 9 || currentBlock[2,3] != 9 || currentBlock[3,3] != 9)
						{
							kollision := 1
						}
						else
						{
							if(currentBlockY >= 21)
							{
								if(currentBlock[0,2] != 9 || currentBlock[1,2] != 9 || currentBlock[2,2] != 9 || currentBlock[3,2] != 9)
								{
									kollision := 1
								}
								else
								{
									if(currentBlockY >= 22)
									{
										kollision := 1
									}
								}
							}
						}
					}
					else
					{
						if(Feld[(currentBlockY -3) +l, currentBlockX +m] =1)
						{
							kollision := 1
						}
						if(Feld[(currentBlockY -3) +l, currentBlockX +m] =2)
						{
							kollision := 1
						}
						if(Feld[(currentBlockY -3) +l, currentBlockX +m] =3)
						{
							kollision := 1
						}
						if(Feld[(currentBlockY -3) +l, currentBlockX +m] =4)
						{
							kollision := 1
						}
						if(Feld[(currentBlockY -3) +l, currentBlockX +m] =5)
						{
							kollision := 1
						}
						if(Feld[(currentBlockY -3) +l, currentBlockX +m] =6)
						{
							kollision := 1
						}
						if(Feld[(currentBlockY -3) +l, currentBlockX +m] =7)
						{
							kollision := 1
						}
					}
				}
				m++
			}
			l++
		}
		if(kollision = 1)
		{
			l := 0
			loop, 4
			{
				m := 0
				Loop, 4
				{
					if(currentBlock[m, l] != 9)
					{
						tmp := (((currentBlockY-4)+l)*10)+currentBlockX+m
						if(currentBlockColor = 1)
						{
							GuiControl,, ted%tmp%, *w20 *h20 rot.png
							Feld[(currentBlockY-4)+l, currentBlockX+m] := currentBlockColor
						}
						if(currentBlockColor = 2)
						{
							GuiControl,, ted%tmp%, *w20 *h20 blau.png
							Feld[(currentBlockY-4)+l, currentBlockX+m] := currentBlockColor
						}
						if(currentBlockColor = 3)
						{
							GuiControl,, ted%tmp%, *w20 *h20 grün.png
							Feld[(currentBlockY-4)+l, currentBlockX+m] := currentBlockColor
						}
						if(currentBlockColor = 4)
						{
							GuiControl,, ted%tmp%, *w20 *h20 gelb.png
							Feld[(currentBlockY-4)+l, currentBlockX+m] := currentBlockColor
						}
						if(currentBlockColor = 5)
						{
							GuiControl,, ted%tmp%, *w20 *h20 orange.png
							Feld[(currentBlockY-4)+l, currentBlockX+m] := currentBlockColor
						}
						if(currentBlockColor = 6)
						{
							GuiControl,, ted%tmp%, *w20 *h20 lila.png
							Feld[(currentBlockY-4)+l, currentBlockX+m] := currentBlockColor
						}
						if(currentBlockColor = 7)
						{
							GuiControl,, ted%tmp%, *w20 *h20 Türkis.png
							Feld[(currentBlockY-4)+l, currentBlockX+m] := currentBlockColor
						}
					}
					m++
				}
				l++
			}
			kollision := 0
			speed := 20
			
			gosub reihenabgleich
			gosub createShape
		}
		currentBlockY++
		k := 0
	}
return

moveDown:
	if(downOnce = 0)
	{
		speed := 1
		downOnce := 1
	}
return

moveRight:
	if(rightOnce = 0)
	{
		if(currentBlockX < 6)
		{
			currentBlockX++
		}
		else
		{
			if(currentBlockX == 6)
			{
				if(currentBlock[3,0] = 9) 
				{
					if(currentBlock[3,1] = 9)
					{
						if(currentBlock[3,2] = 9)
						{
							if(currentBlock[3,3] = 9)
							{
								currentBlockX++
							}
						}
					}
				}
			}
			else
			{
				if(currentBlockX == 7)
				{
					if(currentBlock[2,0] = 9)
					{
						if(currentBlock[2,1] = 9)
						{
							if(currentBlock[2,2] = 9)
							{
								if(currentBlock[2,3] = 9)
								{
									currentBlockX++
								}
							}
						}
					}
				}
			}
			
		}
		rightOnce := 1
	}
return

moveLeft:
	if(leftOnce = 0)
	{
		if(currentBlockX > 0)
		{
			currentBlockX--
		}
		else
		{
			if(currentBlockX == 0)
			{
				if(currentBlock[0,0] = 9) 
				{
					if(currentBlock[0,1] = 9)
					{
						if(currentBlock[0,2] = 9)
						{
							if(currentBlock[0,3] = 9)
							{
								currentBlockX--
							}
						}
					}
				}
			}
			else
			{
				if(currentBlockX == -1)
				{
					if(currentBlock[1,0] = 9) 
					{
						if(currentBlock[1,1] = 9)
						{
							if(currentBlock[1,2] = 9)
							{
								if(currentBlock[1,3] = 9)
								{
									currentBlockX--
								}
							}
						}
					}
				}
			}
			
		}
		leftOnce := 1
	}
return

spinRight:
	if(sOnce = 0)
	{
		i := 0
		loop, 4
		{
			j := 0
			loop, 4
			{
				bufferBlock[i, j] := currentBlock[i, j]
				j++
			}
			i++
		}
		
		currentBlock[0, 0] := bufferBlock[0, 3]
		currentBlock[0, 1] := bufferBlock[1, 3]
		currentBlock[0, 2] := bufferBlock[2, 3]
		currentBlock[0, 3] := bufferBlock[3, 3]
		
		currentBlock[1, 0] := bufferBlock[0, 2]
		currentBlock[1, 1] := bufferBlock[1, 2]
		currentBlock[1, 2] := bufferBlock[2, 2]
		currentBlock[1, 3] := bufferBlock[3, 2]
		
		currentBlock[2, 0] := bufferBlock[0, 1]
		currentBlock[2, 1] := bufferBlock[1, 1]
		currentBlock[2, 2] := bufferBlock[2, 1]
		currentBlock[2, 3] := bufferBlock[3, 1]
		
		currentBlock[3, 0] := bufferBlock[0, 0]
		currentBlock[3, 1] := bufferBlock[1, 0]
		currentBlock[3, 2] := bufferBlock[2, 0]
		currentBlock[3, 3] := bufferBlock[3, 0]
		
		gosub correctPosition
		
		sOnce := 1
	}
return

spinLeft:
	if(aOnce = 0)
	{
		i := 0
		loop, 4
		{
			j := 0
			loop, 4
			{
				bufferBlock[i, j] := currentBlock[i, j]
				j++
			}
			i++
		}
		
		currentBlock[0, 0] := bufferBlock[3, 0]
		currentBlock[0, 1] := bufferBlock[2, 0]
		currentBlock[0, 2] := bufferBlock[1, 0]
		currentBlock[0, 3] := bufferBlock[0, 0]
		
		CurrentBlock[1, 0] := bufferBlock[3, 1]
		CurrentBlock[1, 1] := bufferBlock[2, 1]
		CurrentBlock[1, 2] := bufferBlock[1, 1]
		CurrentBlock[1, 3] := bufferBlock[0, 1]
		
		CurrentBlock[2, 0] := bufferBlock[3, 2]
		CurrentBlock[2, 1] := bufferBlock[2, 2]
		CurrentBlock[2, 2] := bufferBlock[1, 2]
		CurrentBlock[2, 3] := bufferBlock[0, 2]
		
		CurrentBlock[3, 0] := bufferBlock[3, 3]
		CurrentBlock[3, 1] := bufferBlock[2, 3]
		CurrentBlock[3, 2] := bufferBlock[1, 3]
		CurrentBlock[3, 3] := bufferBlock[0, 3]
		
		gosub correctPosition
		
		aOnce := 1
	}
return

correctPosition:
	if(currentBlockX == -2)
		{
			if(currentBlock[0, 0] != 9 || currentBlock[0, 1] != 9 || currentBlock[0, 2] != 9 || currentBlock[0, 3] != 9)
			{
				currentBlockX++
			}
			if(currentBlock[1, 0] != 9 || currentBlock[1, 1] != 9 || currentBlock[1, 2] != 9 || currentBlock[1, 3] != 9)
			{
				currentBlockX++
			}
		}
		else
		{
			if(currentBlockX == -1)
			{
				if(currentBlock[0, 0] != 9 || currentBlock[0, 1] != 9 || currentBlock[0, 2] != 9 || currentBlock[0, 3] != 9)
				{
					currentBlockX++
				}
			}
		}
		if(currentBlockX == 8)
		{
			if(currentBlock[3, 0] != 9 || currentBlock[3, 1] != 9 || currentBlock[3, 2] != 9 || currentBlock[3, 3] != 9)
			{
				currentBlockX--
			}
			if(currentBlock[2, 0] != 9 || currentBlock[2, 1] != 9 || currentBlock[2, 2] != 9 || currentBlock[2, 3] != 9)
			{
				currentBlockX--
			}
		}
		else
		{
			if(currentBlockX == 7)
			{
				if(currentBlock[3, 0] != 9 || currentBlock[3, 1] != 9 || currentBlock[3, 2] != 9 || currentBlock[3, 3] != 9)
				{
					currentBlockX--
				}
			}
		}
return

createShape:
	rand := 0
	speed := 20
	Random, rand, 1, 7
	currentBlockX := 3
	currentBlockY := 0
	
	i := 0
	loop, 4
	{
		j := 0
		loop, 4
		{
			currentBlock[i, j] := nextBlock[i, j]
			j++
		}
		i++
	}
	CurrentBlockColor := nextBlockColor
	
	if(rand = 1)
	{
		;
		;## shape
		;##
		nextBlock[0, 0] := 9
		nextBlock[0, 1] := 9
		nextBlock[0, 2] := 9
		nextBlock[0, 3] := 9
		
		nextBlock[1, 0] := 9
		nextBlock[1, 1] := 0
		nextBlock[1, 2] := 1
		nextBlock[1, 3] := 9
		
		nextBlock[2, 0] := 9
		nextBlock[2, 1] := 3
		nextBlock[2, 2] := 2
		nextBlock[2, 3] := 9
		
		nextBlock[3, 0] := 9
		nextBlock[3, 1] := 9
		nextBlock[3, 2] := 9
		nextBlock[3, 3] := 9
		
		nextBlockColor := rand
	}
	if(rand = 2)
	{
		;
		;#### shape
		;
		nextBlock[0, 0] := 9
		nextBlock[0, 1] := 0
		nextBlock[0, 2] := 9
		nextBlock[0, 3] := 9
		
		nextBlock[1, 0] := 9
		nextBlock[1, 1] := 1
		nextBlock[1, 2] := 9
		nextBlock[1, 3] := 9
		
		nextBlock[2, 0] := 9
		nextBlock[2, 1] := 2
		nextBlock[2, 2] := 9
		nextBlock[2, 3] := 9
		
		nextBlock[3, 0] := 9
		nextBlock[3, 1] := 3
		nextBlock[3, 2] := 9
		nextBlock[3, 3] := 9
		
		nextBlockColor := rand		
	}
	if(rand = 3)
	{
		;#
		;# shape
		;##
		nextBlock[0, 0] := 9
		nextBlock[0, 1] := 9
		nextBlock[0, 2] := 9
		nextBlock[0, 3] := 9
		
		nextBlock[1, 0] := 0
		nextBlock[1, 1] := 1
		nextBlock[1, 2] := 2
		nextBlock[1, 3] := 9
		
		nextBlock[2, 0] := 9
		nextBlock[2, 1] := 9
		nextBlock[2, 2] := 3
		nextBlock[2, 3] := 9
		
		nextBlock[3, 0] := 9
		nextBlock[3, 1] := 9
		nextBlock[3, 2] := 9
		nextBlock[3, 3] := 9
		
		nextBlockColor := rand		
	}
	if(rand = 4)
	{
		; #
		; #  shape
		;##
		nextBlock[0, 0] := 9
		nextBlock[0, 1] := 9
		nextBlock[0, 2] := 9
		nextBlock[0, 3] := 9
		
		nextBlock[1, 0] := 9
		nextBlock[1, 1] := 9
		nextBlock[1, 2] := 0
		nextBlock[1, 3] := 9
		
		nextBlock[2, 0] := 1
		nextBlock[2, 1] := 2
		nextBlock[2, 2] := 3
		nextBlock[2, 3] := 9
		
		nextBlock[3, 0] := 9
		nextBlock[3, 1] := 9
		nextBlock[3, 2] := 9
		nextBlock[3, 3] := 9
		
		nextBlockColor := rand		
	}
	if(rand = 5)
	{
		;##
		; ## shape
		;
		nextBlock[0, 0] := 9
		nextBlock[0, 1] := 9
		nextBlock[0, 2] := 9
		nextBlock[0, 3] := 9
		
		nextBlock[1, 0] := 9
		nextBlock[1, 1] := 0
		nextBlock[1, 2] := 9
		nextBlock[1, 3] := 9
		
		nextBlock[2, 0] := 9
		nextBlock[2, 1] := 1
		nextBlock[2, 2] := 2
		nextBlock[2, 3] := 9
		
		nextBlock[3, 0] := 9
		nextBlock[3, 1] := 9
		nextBlock[3, 2] := 3
		nextBlock[3, 3] := 9
		
		nextBlockColor := rand		
	}
	if(rand = 6)
	{
		; ##
		;## shape
		;
		nextBlock[0, 0] := 9
		nextBlock[0, 1] := 9
		nextBlock[0, 2] := 0
		nextBlock[0, 3] := 9
		
		nextBlock[1, 0] := 9
		nextBlock[1, 1] := 1
		nextBlock[1, 2] := 2
		nextBlock[1, 3] := 9
		
		nextBlock[2, 0] := 9
		nextBlock[2, 1] := 3
		nextBlock[2, 2] := 9
		nextBlock[2, 3] := 9
		
		nextBlock[3, 0] := 9
		nextBlock[3, 1] := 9
		nextBlock[3, 2] := 9
		nextBlock[3, 3] := 9
		
		nextBlockColor := rand		
	}
	if(rand = 7)
	{
		;#
		;## shape
		;#
		nextBlock[0, 0] := 9
		nextBlock[0, 1] := 9
		nextBlock[0, 2] := 9
		nextBlock[0, 3] := 9
		
		nextBlock[1, 0] := 9
		nextBlock[1, 1] := 0
		nextBlock[1, 2] := 1
		nextBlock[1, 3] := 2
		
		nextBlock[2, 0] := 9
		nextBlock[2, 1] := 9
		nextBlock[2, 2] := 3
		nextBlock[2, 3] := 9
		
		nextBlock[3, 0] := 9
		nextBlock[3, 1] := 9
		nextBlock[3, 2] := 9
		nextBlock[3, 3] := 9
		
		nextBlockColor := rand
	}
	gosub changeColor
return


changeColor:
	if(currentBlockColor = 1)
	{
		guiControl,, test0, rot.png
		guiControl,, test1, rot.png
		guiControl,, test2, rot.png
		guiControl,, test3, rot.png
	}
	if(currentBlockColor = 2)
	{
		guiControl,, test0, blau.png
		guiControl,, test1, blau.png
		guiControl,, test2, blau.png
		guiControl,, test3, blau.png
	}
	if(currentBlockColor = 3)
	{
		guiControl,, test0, grün.png
		guiControl,, test1, grün.png
		guiControl,, test2, grün.png
		guiControl,, test3, grün.png
	}
	if(currentBlockColor = 4)
	{
		guiControl,, test0, gelb.png
		guiControl,, test1, gelb.png
		guiControl,, test2, gelb.png
		guiControl,, test3, gelb.png
	}
	if(currentBlockColor = 5)
	{
		guiControl,, test0, orange.png
		guiControl,, test1, orange.png
		guiControl,, test2, orange.png
		guiControl,, test3, orange.png
	}
	if(currentBlockColor = 6)
	{
		guiControl,, test0, lila.png
		guiControl,, test1, lila.png
		guiControl,, test2, lila.png
		guiControl,, test3, lila.png
	}
	if(currentBlockColor = 7)
	{
		guiControl,, test0, türkis.png
		guiControl,, test1, türkis.png
		guiControl,, test2, türkis.png
		guiControl,, test3, türkis.png
	}
	if(nextBlockColor = 1)
	{
		guiControl,, Next0, rot.png
		guiControl,, Next1, rot.png
		guiControl,, Next2, rot.png
		guiControl,, Next3, rot.png
	}
	if(nextBlockColor = 2)
	{
		guiControl,, Next0, blau.png
		guiControl,, Next1, blau.png
		guiControl,, Next2, blau.png
		guiControl,, Next3, blau.png
	}
	if(nextBlockColor = 3)
	{
		guiControl,, Next0, grün.png
		guiControl,, Next1, grün.png
		guiControl,, Next2, grün.png
		guiControl,, Next3, grün.png
	}
	if(nextBlockColor = 4)
	{
		guiControl,, Next0, gelb.png
		guiControl,, Next1, gelb.png
		guiControl,, Next2, gelb.png
		guiControl,, Next3, gelb.png
	}
	if(nextBlockColor = 5)
	{
		guiControl,, Next0, orange.png
		guiControl,, Next1, orange.png
		guiControl,, Next2, orange.png
		guiControl,, Next3, orange.png
	}
	if(nextBlockColor = 6)
	{
		guiControl,, Next0, lila.png
		guiControl,, Next1, lila.png
		guiControl,, Next2, lila.png
		guiControl,, Next3, lila.png
	}
	if(nextBlockColor = 7)
	{
		guiControl,, Next0, türkis.png
		guiControl,, Next1, türkis.png
		guiControl,, Next2, türkis.png
		guiControl,, Next3, türkis.png
	}
return


ButtonHighscores:
Gui, New
Gui, Submit, NoHide
FileRead, Highscore, C:\Users\%UserName%\Desktop\Tetris\TetrisScore.txt
  	
		
Loop, Parse, Highscore, #
	{
	if (A_LoopField != "")
		{
	StringSplit, split, A_LoopField, |
	Spieler := split1
	punkte := split2
	
	Gui, Add, Text, section, %Spieler%
	Gui, Add, Text, W100 Right xs, %punkte%
		}
	}
	Gui, Show,, Score

return

SortMethod(a, b)
{
StringSplit, split, a, |
aName := split1
aPunkte := split2
StringSplit, split, b, |
bName := split1
bPunkte := split2

return aPunkte < bPunkte
}
return

ButtonOptions:
Gui, New
Gui, Font,, Modern No. 20 
Gui, Add, Text,, UserName
Gui, Add, Edit, w100 r1 vUserName
Gui, Add, Button, gButtonSaveName, Save
Gui, Show, w120 h150
return

ButtonSaveName:
Gui,Submit, NoHide
FileDelete, TetrisUserName.txt
FileAppend, %UserName%, C:\Users\%UserName%\Desktop\Tetris\TetrisUserName.txt
return

ButtonExit:
ExitApp
return

ButtonRestart:
Gui, Destroy
Goto, ButtonGo

ButtonSave:
Gui, Submit, NoHide
FileRead, Highscore, C:\Users\%UserName%\Desktop\Tetris\TetrisScore.txt
IfInString, Highscore, %Name%
  {
  temp := Highscore . "#"
  StringGetPos, startPos, temp, %Name%|
  StringGetPos, endPos, temp, #,,%startPos%
  startPos := startPos + 4
  laenge := endPos - startPos + 1
  StringMid, punkte, temp, %startPos%, %laenge%
  if (Score> punkte)
    {
    StringReplace, Highscore, Highscore, %Name%|%punkte%, %Name%|%Score%
	}
  }
	Else
			{
			Highscore = %Highscore%#%Name%|%Score%
			}
			If InString (Highscore, #, StartingPos= -1, Occurence= 1)
				{
			
				}	
		Sort, Highscore, D# F SortMethod
FileDelete, C:\Users\%UserName%\Desktop\Tetris\TetrisScore.txt
FileAppend, %Highscore%, C:\Users\%UserName%\Desktop\Tetris\TetrisScore.txt

return

#q::
ExitApp