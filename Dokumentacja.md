#### Kreacyjne:
- Singleton - Jest możliwość istnienia tylko jednego poziomu w danym momencie,
poziom jest tworzony w momencie rozpoczęcia gry, usuwany po jego przejściu przez
gracza, oraz tworzony nowy (z innymi parametrami, kolejny poziom). (Wykoszystywana
będzie późna inicjalizacja)
- Multiton - ProjectilePools jest multitonem zawierającym parę rodzai ProjectilePool'i.
- W grze będzie istnieć parę rodzajów pocisków do których trzebaby tworzyć
odpowiednie object pool'e ale można to rozwiązać za pomocą multitona który będzie
tworzył i podawał odpowiednie pool'e na potrzeby wykorzystania pocisków w nich
zawartych.
- Abstract Factory - IWeapon będą fabrykami pocisków.
    - W grze spaceship wydając komendy turretom, by te wystrzeliły pociski powinien
nie musieć wiedzieć jakiego typu pociski wystrzeliwuje i jedynie korzystać do tego z
interfejsu IWeapon.
- Object Pool - Pociski będą przechowywane w puli,
- W grze będzie potrzeba wytworzenia wielu pocisków w krótkim czasie, jednak
te pociski mają krótki okres życia (nie dłużej niż kilka sekund).
- Tworzenie GameObject'ów w Unity to stosunkowo kosztowny proces, który przy
dużej ilości takich operacji na sekunde może znacząco wpłynąć na płynność gry.
- Pociski będą przechowywane w puli która to na życzenie (wywołanie api),
będzie podawać (lub jeśli zabraknie w puli tworzyć) obiekty do każdego miejsca gdzie są
one wymagane. Później te obiekty są odpowiednio przygotowywane przez metodę
fabrykującą.
- Po skończonym czasie życia, obiekty zamiast ulegać destrukcji i zebraniu przez
garbage collector, będą wracały do puli.
#### Strukturalne:
- Flyweight - Projectile będą pyłkami, których główne dane będą przechowywane w
IWeapon,
    - W grze będzie potrzeba wygenerowania dużej ilości pocisków które będą
współdzieliły dużo właściwości takich jak ich prędkość, sprite.
- Composite - ITurret.
    - ITurret jest interfejsem z którego korzysta spaceship kiedy chce sterować
wieżyczką, może to robić kontrolując bezpośrednio każdą z wieżyczek, albo te wieżyczki
mogą być zgrupowane w TurretGroup, jednak z punktu widzenia spaceship, podaje
jedynie cel i komende prowadzenia ognia.
#### Czynnościowe:
- Strategy - Różne statki przeciwnika będą miały różne zachowanie (przeciwnicy są tu
kontekstem, wykorzystujemy metodę push (statki nie wiedzą jakiej strategii używają)
    - Statki będą podawać do strategii swój stan, a ta będzie zwracać co taki statek
ma zrobić.
    - Statki będą miały np. strategię, by za wszelką cenę skracać dystans do gracza,
albo inną strategią będzie trzymanie się na dystans.
- Observer - Klasa DisplayStats obserwuje zmiany spaceship i uaktualnia interfejs
użytkownika.