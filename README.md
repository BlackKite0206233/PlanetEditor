# PlanetEditor
A planet editor based on C#, NTUST CSIE GameLab Assignment

Q : 什麼是Class？
A : 他像是一個範本，透過方法、屬性來規定Object生成後會長什麼樣子，有3個很重要的特性，封裝、繼承、多型。

Q : 什麼是Constructor？
A : 要將Class實體化成Object時會用到的function。

Q : 什麼是Destructor？
A : 要將這個Object刪除時會用到的function，目的是記憶體回收。

Q : 什麼是Interface？
A : 有點像一個Class，但是沒辦法實體化，他只是定義了哪些function該被實作，通常是拿來被其他Class引用。

Q : 什麼是Polymorphism？ 我在哪使用了Polymorphism？
A : 子類別可以繼承父類別的function，也可以透過override或virtual，針對特定的子類別改寫function，讓每個子類別有不同的操作。
    在CreatureType跟他的子類別Lion、Plant有用到，還有在Object跟他的子類別Planet、Creature。
    
Q : 什麼是template？
A : 樣板，類似C#的泛型，能讓Class或function傳入不同的資料型別，這樣就不用根據不同資料型別寫不同的程式碼。

Q : Planet::Planet(const Planet & src)將會產生什麼問題？
A : 

Q : std::shared_ptr是什麼？
A : C++的smart point的一種，他允許很多個shared_ptr指到同一塊記憶體，只要那塊記憶體還有被任何一個shared_ptr指到，他就不會被釋放，等到沒有任何shared_ptr指到那塊記憶體，快記憶體才會被釋放。這樣可以確保程式比較不會出錯。

Q : 如果忘記釋放記憶體會發生什麼事？ C#會發生相同的事嗎？
A : 忘記釋放記憶體，那個變數的生命周期又結束的話，會造成memory leak，就是一塊記憶體佔在那邊，但是完全沒被使用到。
    C#有自動回收的功能，所以不會有這個問題，但是如果都不自己釋放，全部交給系統的話，會造成程式執行效率變低。
