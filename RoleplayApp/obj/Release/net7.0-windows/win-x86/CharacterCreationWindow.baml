   M S B A M L   `   `   `  ���� F  BRoleplayApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RoleplayAppRoleplayApp  )    #RoleplayApp.CharacterCreationWindow   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:RoleplayApp     
�������  � Title$  CharacterCreationWindow��$	��600��$
��1000��.���  � RowDefinitions �� $��* ��� $��* ��� $��* � � ColumnDefinitions �� $��* ��� $��* ��� $��* �.+��� $��3��$��3q�
���iii�$	��270��$	��543��$��Bottom=�._��� $	��Top=�.+��� $��16(�$��0,0,0,5q�$%��Hvor mange liv? (Skriv kun tal)�� �� TextAlignment$ Center=��� -   $��WriteHealth��$��20(�$	��100���� $��16(�$��0,5,0,5q�$)��#Hvor meget defence? (Skriv kun tal)��$ Center=��� -   $��WriteDefence��$��20(�$	��100���� $��16(�$��0,5,0,5q�$)��#Hvor meget Agility? (Skriv kun tal)��$ Center=��� -   $��WriteAgility��$��20(�$	��100���� $��16(�$��0,5,0,5q�$(��"Hvor meget Styrke? (Skriv kun tal)��$ Center=��� -   $��WriteStrength��$��20(�$	��100���� $��16(�$��0,5,0,5q�$*��$Hvor meget Intelect? (Skriv kun tal)��$ Center=��� -   $��WriteIntelect��$��20(�$	��100���� $��16(�$��0,5,0,5q�$)��#Hvor meget Karisma? (Skriv kun tal)��$ Center=��� -   $��WriteCharisma��$��20(�$	��100���� $��12(�$��0,5,0,0q�$*��$1 = Dårligst muligt for et menneske���� $��12(�$��2 - 3 = Frygteligt svag���� $��12(�$��4 - 5 = Under gennemsnit���� $��12(�$#��6 - 7 = Lidt under gennemsnit���� $��12(�$��8 - 9 = Gennemsnitlig���� $��12(�$$��10 - 11 = Lidt over gennemsnit���� $��12(�$��12 - 13 = Over gennemsnit���� $��12(�$%��14 - 15 = Meget over gennemsnit���� $��12(�$��16 - 17 = Enestående���� $��12(�$*��$18 = Næsten perfekt for et menneske���� $��12(�$��19 - 20 = Direkte OP���� $��1��$��3��$	��270��$	��500��$��3q�
���iii�._��� .+��� $��
Skriv navn��$��16(�$��Center=�$��0,30,0,0q��� -   $��	WriteName��$��20(��� $��Level (Skriv kun tal)��$��16(�$��Center=�$��0,10,0,0q��� -   $��
WriteLevel��$��20(��� $��Alder (Skriv kun tal)��$��16(�$��Center=�$��0,10,0,0q��� -	   $��WriteAge��$��20(�$	��100����  �� Orientation$ 
Horizontal=�$��Center=�$��0,10,0,0q�.+��� -
   $��
genderMale��$��35��$	��100��$
��Mand��$��20(�
����·��� -   $��genderFemale��$��35��$	��100��$��Kvinde��$��20(�
��������� $��16(�$��0,10,0,5q�$��Tryk og vælg race��$ Center=��� -   $
��Type��$��2��$��3��$	��Top=�$��Center=�$	��200��$��21��$��0,10,0,0q�.@��� $��Menneske���� $��Robot���� $��Drage���� $��Ukendt���� $&�� Skriv landet personen kommer fra��$��16(�$��Center=�$��0,10,0,0q��� -   $��WriteCountry��$��20(�$	��100���� $��2��$��3��$	��270��$	��540��$��3q�$��Bottom=�
���iii�._��� .+��� $��Skriv våben��$��16(�$��Center=�$��0,10,0,0q��� -   $��WriteWeapon��$��20(��� $��Skriv penge��$��16(�$��Center=�$��0,10,0,0q��� -   $��
WriteMoney��$��20(��� $*��$Skriv en beskrivelse (Behøves ikke)��$��16(�$��Center=�$��0,10,0,0q��� -   $��WriteDescription��$	��100��$��12(� �� TextWrapping$
 Wrap=��� $ 
Horizontal=�.+��� $	��120��$��132.5��
���iii�$��2q�._��� .+��� -   $��20��$��Tilføj Ven���� -   $��FriendsList������ *	Friends+���� .R��� -   $��14(����� *Friend+�� $	��120��$��132.5��
���iii�$��2q�._��� .+��� -   $��20��$��Tilføj Fjende���� -   $��EnemiesList������ *	Enemies+���� .R��� -   $��14(����� *Enemy+�� $#��I disse, kan du skrive skills��$��16(�$��Center=�$��0,10,0,0q��� -   $��Tilføj Skill��$��16(��� -   $��
SkillsList������ *Skills+���� .R��� -   $��14(����� *Skill+�� -   $
��Done��$
��DONE��$��40��$	��200��$��1��$��2��$��Bottom=��� $��1��$��2��$	��200��$��	0,60,0,50q�.+��� -   $��AddImage��$��Tilføj billede��$��20(�$��30��$	��200��$��Center=��� -   $��ImageUploadText��$��14(�
��� � �$��Center=��� -    ��Name$ GoBack��$��25��$��50��$	��Top=�$
��Left=�$
��Back��$��15(�$
��Bold"����#dc3131