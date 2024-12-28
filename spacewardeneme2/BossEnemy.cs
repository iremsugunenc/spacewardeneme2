using spacewardeneme2;
using System;

public class BossEnemy : Enemy
{
    // BossEnemy nin hasarını tutan özellik
    public int Damage { get; set; }
    public Bullet Bullet { get; set; }

    public BossEnemy(int x, int y, int health, int speed, int damage) : base(x, y, health, speed, damage)
    {  
        // BossEnemy'nin özel hasarını ayarlar
        Damage = damage;
    }

    // BossEnemy nin resmini döndüren metod
    public override Image GetImage()
    {
        return spacewardeneme2.Properties.Resources.Gemi_07; 
    }
    // BossEnemy'nin hareketini tanımlar
    public override void Move(int playerX, int playerY)
    {
        // BossEnemy sadece yatay hareket eder
        // Eğer BossEnemynin x konumu oyuncunun x konumundan büyükse sola hareket eder
        // Eğer BossEnemynin x konumu oyuncunun x konumundan küçükse sağa hareket eder
        if (X > playerX)
        {
            X -= Speed;
        }
        else if (X < playerX)
        {
            X += Speed;
        }
    }
    // BossEnemy'nin ateş etmesini sağlar
    public override Bullet Shoot()
    {
        // Mermi sola doğru hareket eder, hız ve hasar belirlenmiştir
        return new Bullet(X + 35, Y + 50, 10, Direction.Left, 10);
    }
}