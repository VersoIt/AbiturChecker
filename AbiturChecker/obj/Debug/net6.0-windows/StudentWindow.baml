   M S B A M L   `   `   `  ????H  DAbiturChecker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AbiturChecker.CoreAbiturChecker.Core  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5   	   !    AbiturChecker.StudentWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:AbiturChecker.Core     5   	   Bcol2clr-namespace:System.Collections;assembly=mscorlib     5   	   $
??1000??5	   	   $	??450??6     ? WindowStartupLocation$  CenterScreen=?5
   	    ? Title$ StudentWindow??5   	   $	??450??6   $	??800??6,   .??5      ? 
??????? ? RowDefinitions 5   
   ?? 5      $??* ?6   6)   ?? 5      $	??10* ?6   6+   .+?5   
   ?? -   5        $??Header??5      $??Center=?6"   $??14(?6=    ?? TextAlignment$ Center=?6K   
?????_?86ИТМО - Прикладная информатика6?   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ StudentsTable??5   ?   $??1??6      
IsReadOnly$
 True??6       SelectionMode$ Single=?62      SelectionUnit$ FullRow=?6I      CanUserResizeRows$ False??6a   	   CanUserAddRows$	 False??6{   
   CanUserSortColumns$

 True??6?      AutoGenerateColumns$ False??6?      HorizontalGridLinesBrush
 ?????   AlternatingRowBackground
 ?????   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   MinWidth$ 88Q?6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 63      Header$ 
Снилс??6=   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6L   ??  ?? Path$ Id?5        5      $ 90Q?6%   $ * 63   $ Сумма баллов??6=    6S   ?? $	 Sum?  5      $	 140Q?6%   $ * 64   $0 *Согласие на зачисление??6>    6^   ?? $ IsAgree?5   
   5      5      