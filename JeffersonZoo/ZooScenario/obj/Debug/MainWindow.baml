   M S B A M L   `   `   `  ????F  BZooScenario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ZooScenarioZooScenario  5   	       ZooScenario.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ZooScenario     5   	   -   5          ? Title$(  "Object-Oriented Programming 2: Zoo??5   	   $??350.767??64   $	??683??6E    ? WindowStartupLocation$ CenterScreen=?6Q   .??5	      ? -   5         ?Name$ window??5	       ? RowDefinitions 5
   
   ?? 5      $
??161* ?6   ?? 5      $
??246* ?6   .+?5   
   ?? -   5         ??Name$ admitGuestButton??5      $??Admit guest??6+   $
??Left=?6A   $??5,10,0,0q?6\   $	??Top=?6n   $	??131??6?   6?   ?? 5   
   -   5        $ feedAnimalButton??5      $??Feed animal??6+   $
??Left=?6A   $??5,36,0,0q?6\   $	??Top=?6n   $	??131??6?   6?   ?? 5   
   -   5         ??Name$ temperatureBorder??5      
???   ?$??1q?6@   $??Right=?6T   $	??100??6p   $??0,0,10.333,9.667q?6}   $??Bottom=?6?   $??20??6?   
???  ??$??1??6?   ?? 5   
   -   5         ??Name$ increaseTemperatureButton??5      $??+??6:   $??Right=?6F   $??0,0,35.333,37.667q?6b   $??Bottom=?6}   $??23??6?   $??23??6?    J? RenderTransformOrigin$ 0.514,0.514&?6?   $??1??6?   ?? 5   
   -   5        $ decreaseTemperatureButton??5      $??-??6:   $??Right=?6F   $??0,0,35.333,9.667q?6b   $??Bottom=?6|   $??23??6?   $??23??6?   $ 1.667,0.435&?6?   $??1??6?   ?? 5   
   -   5         ??Name$ temperatureLabel??5      $
??Temp??6*   $??Right=?69   $??0,0,30.333,65.667q?6U   $??Bottom=?6p   $??1??6?   ?? 5   
   -	   5        	 ??Name$	 animalListBox??5      $
??Left=?6)   $	??220??6D   $??
141,36,0,0q?6Q   $	??Top=?6e   $	??196??6}   $??2??6?   6?   ?? 5   
   -
   5        $	 guestListBox??5      $
??Left=?6(   $	??220??6C   $??
345,36,0,0q?6P   $	??Top=?6d   $	??196??6|   $??2??6?   6?   ?? 5   
   -   5        $ animalListBoxLabel??5      $??Animals??6,   $
??Left=?6>   $??	141,7,0,0q?6Y   $	??Top=?6l   ?? 5   
   -   5        $ guestListBoxLabel??5      $??Guests??6+   $
??Left=?6<   $??	345,7,0,0q?6W   $	??Top=?6j   ?? 5   
   -   5        $ addAnimalButton??5      $	??Add??6*   $
??Left=?68   $??198,135.667,0,0q?6S   $??1??6l   $	??Top=?6y   $??52??6?   6?   ?? 5   
   -   5        
 ??Name$
 animalTypeComboBox??5      $
??Left=?6/   $??255,133.667,0,0q?6J   $	??Top=?6c   $??82??6{   $??1??6?   ?? 5   
   -   5        $ removeAnimalButton??5      $??Remove??6-   $
??Left=?6>   $??141,135.667,0,0q?6Y   $??1??6r   $	??Top=?6   $??52??6?   6?   ?? 5   
   -   5        $ removeGuestButton??5      $??Remove??6,   $
??Left=?6=   $??345,133.667,0,0q?6X   $??1??6q   $	??Top=?6~   $??52??6?   6?   ?? 5   
   -   5        $ addGuestButton??5      $	??Add??6)   $
??Left=?67   $??402,133.667,0,0q?6R   $??1??6k   $	??Top=?6x   $??52??6?   6?   ?? 5   
   -   5        $ showCageButton??5      $??	Show cage??6)   $
??Left=?6=   $??21??6X   $??5,62,0,0q?6d   $	??Top=?6v   $	??131??6?   6?   ?? 5   
   -   5        $ adoptAnimalButton??5      $??Adopt animal??6,   $
??Left=?6C   $??5,88,0,0q?6^   $	??Top=?6p   $	??131??6?   6?   ?? 5   
   -   5        $ unadoptAnimalButton??5      $??Unadopt animal??6.   $
??Left=?6G   $??	5,114,0,0q?6b   $	??Top=?6u   $	??131??6?   $??2??6?   6?   ?? 5    
   -   5        $ changMoveBehaviorButton??5       $??Change behavior??62   $
??Left=?6L   $??141,163.667,0,0q?6g   $??1??6?   $	??Top=?6?   $	??109??6?   6?   ?? 5!   
   -   5        $ 
 changeMoveBehaviorComboBox??5!      $
??Left=?67   $??255,163.667,0,0q?6R   $??1??6k   $	??Top=?6x   $??82??6?   ?? 5"   
   -   5        $ birthAnimalButton??5"      $??Birth animal??6,   $
??Left=?6C   $??5,12.667,0,0q?6^   $??1??6t   $	??Top=?6?   $	??131??6?   6?   ?? 5#   
   -   5        $ 
saveButton??5#      $
??Save??6%   $
??Left=?64   $??5,102.667,0,0q?6O   $??1??6f   $	??Top=?6s   $??75??6?   6?   ?? 5$   
   -   5        $ 
loadButton??5$      $
??Load??6%   $
??Left=?64   $??5,128.667,0,0q?6O   $??1??6f   $	??Top=?6s   $??75??6?   6?   ?? 5%   
   -   5        $ restartButton??5%      $??Restart??6(   $
??Left=?6:   $??5,154.667,0,0q?6U   $??1??6l   $	??Top=?6y   $??75??6?   6?   5&      5'      