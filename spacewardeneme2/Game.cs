using spacewardeneme2;
using System.Collections.Generic;
using System.Windows.Forms;


public class Game
{
    public SpaceShipcs player;  // Oyuncunun uzay gemisi
    public List<Enemy> enemies;  // Düşmanlar listesi
    public bool isGameOver;  // Oyun bitti mi kontrolü
    private System.Windows.Forms.Timer gameTimer;  // Oyun zamanlayıcısı

    public Game()
    {
        player = new SpaceShipcs(100, 100, 10, 3);  // Başlangıç koordinatları ve özellikler
        enemies = new List<Enemy>();
        isGameOver = false;

        gameTimer = new System.Windows.Forms.Timer();
        gameTimer.Interval = 50;  // Oyun güncellemesi her 50ms'de bir yapılacak
        gameTimer.Tick += GameTimer_Tick;  // Timer tick event'ini bağla
        gameTimer.Start();
    }

    public void StartGame()
    {
        // Oyuncunun başlangıç konumu ve sağlığı
        player.X = 200;
        player.Y = 400;
        player.Health = 100;

        // Düşmanları temizle ve yeni düşmanlar ekle
        enemies.Clear();
        enemies.Add(new BasicEnemy(300, 100, 3, 3, 10));
        enemies.Add(new FastEnemy(400, 200, 5, 10, 10));

        isGameOver = false;

        // Zamanlayıcıyı başlat
        gameTimer.Start();
    }

    public void UpdateGame()
    {
        if (isGameOver)
            return;

        // Düşmanların ve oyuncunun hareketini güncelle
        foreach (var enemy in enemies)
        {
            // Düşman hareketi
            enemy.Move(player.X, player.Y);

            // Düşmanla çarpışma kontrolü
            if (player.Bounds.IntersectsWith(enemy.Bounds))
            {
                player.Health -= enemy.Damage;  // Oyuncunun sağlığı düşmanla çarpışınca azalır
                break;
            }

            // Düşmanların mermi atma işlemi
            Bullet enemyBullet = enemy.Shoot();
            enemyBullet.Move();

            // Düşman mermisinin çarpışmasını kontrol et
            if (enemyBullet.Bounds.IntersectsWith(new Rectangle(player.X, player.Y, 50, 50)))
            {
                player.Health -= enemyBullet.Damage;  // Oyuncunun sağlığı belirlenen enemy hasarı kadar azalır
                enemyBullet.OnHit();  // Mermiyi yok et
            }
        }

        // Oyuncunun mermisinin hareketini kontrol et
        Bullet playerBullet = player.Shoot(Direction.Right);
        playerBullet.Move();

        // Düşmanlarla çarpışma kontrolü
        foreach (var enemy in enemies)
        {
            if (playerBullet.Bounds.IntersectsWith(enemy.Bounds))
            {
                enemy.Health -= playerBullet.Damage;
                playerBullet.OnHit();  // Mermiyi yok et
            }
        }

        // Oyun bitişini kontrol et
        CheckGameOver();
    }

    public void CheckGameOver()
    {
        // Eğer oyuncunun sağlığı 0 veya daha düşükse oyun biter
        if (player.Health <= 0)
        {
            isGameOver = true;
            EndGame();
        }
    }

    public void EndGame()
    {
        // Skorları kaydetmek ve göstermek için skor tablosunu oluştur
        List<Tuple<string, int>> scoreBoard = new List<Tuple<string, int>>()
        {
            new Tuple<string, int>("Player", 100)  
        };

        // Skorları azalan sıraya göre sıralayıp ilk 5 skoru al
        scoreBoard = scoreBoard
            .OrderByDescending(x => x.Item2)
            .Take(5)
            .ToList();

        // Skor tablosunu oluştur
        string scoreTable = "En İyi 5 Skor:\n";
        for (int i = 0; i < scoreBoard.Count; i++)
        {
            scoreTable += $"{i + 1}. {scoreBoard[i].Item1}: {scoreBoard[i].Item2} puan\n";
        }

        string filePath = @"C:\Users\İrem\Desktop\denemespace\skorlar.txt"; // Skorların yazılacağı dosyanın yolu
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine("En İyi 5 Skor:");
            foreach (var score in scoreBoard)
            {
                writer.WriteLine($"{score.Item1}: {score.Item2} puan");
            }
        }

        // Skor tablosunu ve oyun bitiş mesajını göster
        MessageBox.Show($"Oyun Bitti!\nNedeni: oyuncu öldü\n\n{scoreTable}",
            "Oyun Bitti",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    private void GameTimer_Tick(object sender, EventArgs e)
    {
        UpdateGame();  // Oyun her 50ms'de bir güncelleniyor
    }
}
