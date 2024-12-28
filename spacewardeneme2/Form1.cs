using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace spacewardeneme2
{
    public partial class Form1 : Form
    {
        int formYukseklik = 800, formGenislik = 1500;
        private SpaceShipcs oyuncu;
        private List<Enemy> enemies; // Düşmanları tutacak liste
        private List<Bullet> enemyBullets;
        private Bullet playerBullet;
        private bool isGameStarted = false;
        private int playerScore = 0;
        private List<Tuple<string, int>> scoreBoard = new List<Tuple<string, int>>();
        private Dictionary<string, int> enemyKillCounts = new Dictionary<string, int>();
        private System.Windows.Forms.Timer enemyMoveTimer;
        private System.Windows.Forms.Timer enemySpawnTimer;
        private System.Windows.Forms.Timer bulletMoveTimer;
        private System.Windows.Forms.Timer enemyShootTimer;
        private System.Windows.Forms.Timer enemyBulletMoveTimer;
        private System.Windows.Forms.Timer powerUpMoveTimer;
        private System.Windows.Forms.Timer powerUpTimer;
        private PowerUp powerUp;
        private List<PowerUp> powerUps;
        Random rand = new Random();
        private int Powerupspeed = 50;
        
        public Form1()
        {
            InitializeComponent();

            rand = new Random();

            // Düşmanlar için rastgele X ve Y koordinatları
            int randomX1 = rand.Next(formGenislik - 100, formGenislik - 100);
            int randomY1 = rand.Next(50, formYukseklik - 50);
            int randomX2 = rand.Next(formGenislik - 100, formGenislik - 100);
            int randomY2 = rand.Next(50, formYukseklik - 50);

            // Oyuncu ve düşmanlar

            enemyBullets = new List<Bullet>();
            oyuncu = new SpaceShipcs(200, 400, damage: 10, speed: 0);
            powerUps = new List<PowerUp>();

            enemies = new List<Enemy>
            {
                new BasicEnemy(randomX1, randomY1, 3,3,10),
                new FastEnemy(randomX2, randomY2, 5,15,10)
            };

            // Düşman hareket zamanlayıcısı
            enemyMoveTimer = new System.Windows.Forms.Timer();
            enemyMoveTimer.Interval = 100;
            enemyMoveTimer.Tick += EnemyMoveTimer_Tick;
            enemyMoveTimer.Start();

            // Yeni düşman oluşturma zamanlayıcısı
            enemySpawnTimer = new System.Windows.Forms.Timer();
            enemySpawnTimer.Interval = 3000;
            enemySpawnTimer.Tick += EnemySpawnTimer_Tick;
            enemySpawnTimer.Start();

            // Oyuncu mermisi hareket zamanlayıcısı
            bulletMoveTimer = new System.Windows.Forms.Timer();
            bulletMoveTimer.Interval = 5;
            bulletMoveTimer.Tick += BulletMoveTimer_Tick;
            bulletMoveTimer.Start();

            // Düşman ateş zamanlayıcısı
            enemyShootTimer = new System.Windows.Forms.Timer();
            enemyShootTimer.Interval = 3000;
            enemyShootTimer.Tick += EnemyShootTimer_Tick;
            enemyShootTimer.Start();

            // Düşman mermisi hareket zamanlayıcısı
            enemyBulletMoveTimer = new System.Windows.Forms.Timer();
            enemyBulletMoveTimer.Interval = 30;
            enemyBulletMoveTimer.Tick += EnemyBulletMoveTimer_Tick;
            enemyBulletMoveTimer.Start();

            powerUpMoveTimer = new System.Windows.Forms.Timer();
            powerUpMoveTimer.Interval = 1000;
            powerUpMoveTimer.Tick += PowerUpMoveTimer_Tick;
            powerUpMoveTimer.Start();

            powerUpTimer = new System.Windows.Forms.Timer();
            powerUpTimer.Interval = 20000; 
            powerUpTimer.Tick += PowerUpTimer_Tick;
            powerUpTimer.Start();
        }
        private void startButton_Click(object sender, EventArgs e)
        {

            if (!isGameStarted) // Eğer oyun zaten başlamadıysa
            {
                isGameStarted = true; // Oyun başlatılır
                enemyMoveTimer.Start();
                enemySpawnTimer.Start();
                bulletMoveTimer.Start();
                enemyShootTimer.Start();
                enemyBulletMoveTimer.Start();
                StartGame();
                startButton.Visible = false;

            }
        }
        // Oyunu başlatan metod
        private void StartGame()
        {
           
            // Sağlık bilgisi girilir
            label1.Text = "Oyuncu Sağlık: 100";

            // Düşman sayısı ilk başta 0 olarak gösterilir
            label6.Text = "Düşman Sayısı: 0";

            // Uzay gemisini başlat
            SpaceShip.Location = new Point(28, 195); // Uzay gemisini başlangıç noktasına yerleştir
            SpaceShip.Visible = true;

            //mermi ve diğer öğeleri başlat
            pictureBox5.Location = new Point(179, 219);
            pictureBox5.Visible = true;

            // Oyuncu hareketini başlatmak için gerekli kontrolleri aç
            this.Focus(); // tuşlar ile hareket etmeyi sağlar


           
        }

        private void EndGame(string reason)
        {
            // Oyunu durdur
            enemyMoveTimer.Stop();
            enemySpawnTimer.Stop();
            bulletMoveTimer.Stop();
            enemyShootTimer.Stop();
            enemyBulletMoveTimer.Stop();
            powerUpMoveTimer.Stop();  
            powerUpTimer.Stop();  

            // PowerUp'ları temizle
            foreach (var powerUp in powerUps)
            {
                if (powerUp.PictureBox != null)
                {
                    powerUp.PictureBox.Visible = false;
                    powerUp.PictureBox.Dispose();
                }
            }
            powerUps.Clear();  // Listeyi temizle

            // Oyuncu skorunu tabloya ekle 
            string playerName = "Oyuncu";
            scoreBoard.Add(new Tuple<string, int>(playerName, playerScore));

            // Skor tablosunu sıralayıp ilk 5 skoru tut
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
            string filePath = @"C:\Users\İrem\Desktop\spacewaryeni\skorlar.txt"; // Skorların yazılacağı dosyanın yolu
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine("En İyi 5 Skor:");
                foreach (var score in scoreBoard)
                {
                    writer.WriteLine($"{score.Item1}: {score.Item2} puan");
                }
            }

            // Skor tablosunu ve oyun bitiş mesajını göster
            MessageBox.Show($"Oyun Bitti!\nNedeni: {reason}\n\n{scoreTable}",
                "Oyun Bitti",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            // Oyun durumunu sıfırla
            ResetGame();
        }
        private void AddEnemyKill(string enemyType)
        {
            if (!enemyKillCounts.ContainsKey(enemyType))
            {
                enemyKillCounts[enemyType] = 0;
            }
            enemyKillCounts[enemyType]++;

            // Puanı hesapla ve playerScore a ekle
            int scoreForKill = GetPointValueForEnemy(enemyType);
            playerScore += scoreForKill;

        }
        // enemy türlerini öldürünce gelen skor puanları
        private int GetPointValueForEnemy(string enemyType)
        {
            return enemyType switch
            {
                "FastEnemy" => 100,
                "BasicEnemy" => 50,
                "StrongEnemy" => 200,
                "BossEnemy" => 1000,
                _ => 0
            };
        }
        private void ResetGame()
        {
            // Oyuncuyu ilk haline getir
            oyuncu = new SpaceShipcs(200, 400, damage: 10, speed: 0);
            SpaceShip.Location = new Point(oyuncu.X, oyuncu.Y);

            // Skoru sıfırla
            playerScore = 0;

            // Düşman listesini sıfırla
            foreach (var enemy in enemies)
            {
                enemy.PictureBox.Visible = false;
                enemy.PictureBox.Dispose();
            }
            enemies.Clear();

            // Düşman mermilerini sıfırla
            foreach (var bullet in enemyBullets)
            {
                bullet.PictureBox.Visible = false;
                bullet.PictureBox.Dispose();
            }
            enemyBullets.Clear();

            // Oyuncu mermisini sıfırla
            if (playerBullet != null && playerBullet.PictureBox != null)
            {
                playerBullet.PictureBox.Visible = false;
                playerBullet.PictureBox.Dispose();
                playerBullet = null;
            }

            // Oyuncunun sağlığını ilk hale getir
            oyuncu.Health = 100;

            // Skor tablosunu temizle
            enemyKillCounts.Clear();

            // Zamanlayıcıları yeniden başlat
            enemyMoveTimer.Start();
            enemySpawnTimer.Start();
            bulletMoveTimer.Start();
            enemyShootTimer.Start();
            enemyBulletMoveTimer.Start();

            // Ekrandaki etiketleri sıfırla
            UpdateLabels();

            // Oyunu başlat butonunu göster
            startButton.Visible = true;
            isGameStarted = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = formYukseklik;
            this.Width = formGenislik;
            isGameStarted = false; // Oyun ilk başta başlamamalı
            enemyMoveTimer.Stop();
            enemySpawnTimer.Stop();
            bulletMoveTimer.Stop();
            enemyShootTimer.Stop();
            enemyBulletMoveTimer.Stop();


            // Oyuncu için PictureBox
            SpaceShip.Location = new Point(oyuncu.X, oyuncu.Y);
            SpaceShip.Parent = pictureBoxGalaxy;

            // Düşmanlar için PictureBoxlar
            foreach (var enemy in enemies)
            {
                PictureBox enemyBox = new PictureBox
                {
                    Size = new Size(50, 50),
                    Location = new Point(enemy.X, enemy.Y),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Parent = pictureBoxGalaxy
                };
                enemyBox.SizeMode = PictureBoxSizeMode.StretchImage;
                enemyBox.Image = enemy.GetImage(); // Düşman resmini ayarla
                enemy.PictureBox = enemyBox; // PictureBox referansını düşmana bağla
            }

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.KeyUp += new KeyEventHandler(Form1_KeyUp);
        }

        private void EnemyMoveTimer_Tick(object sender, EventArgs e)
        {
            if (!isGameStarted) return;

            // Silinecek düşmanları tutacak bir liste
            List<Enemy> enemiesToRemove = new List<Enemy>();

            // Düşmanları hareket ettir
            foreach (var enemy in enemies.ToList()) // enemies listesini kopyalayarak gezelim
            {
                // Düşmanı hareket ettir
                enemy.Move(oyuncu.X, oyuncu.Y);
                enemy.UpdatePictureBoxLocation();

                // Çarpışma kontrolü: Oyuncunun uzay aracı ile düşmanların çarpışması
                if (oyuncu.Bounds.IntersectsWith(enemy.Bounds))  // Eğer çarpışma varsa
                {
                    // Oyuncuya hasar ver
                    oyuncu.TakeDamage(20);

                    // Düşmanı silinecekler listesine ekle
                    enemiesToRemove.Add(enemy);
                    AddEnemyKill(enemy.GetType().Name);  // Düşman öldü, skoru güncelle

                    // Eğer oyuncu sağlığı 0 ise oyun bitir
                    if (oyuncu.Health <= 0)
                    {
                        EndGame($"Oyuncu öldü. Skorunuz: {playerScore}");
                    }
                }
            }

            // Silinecek düşmanları listeden çıkar
            foreach (var enemy in enemiesToRemove)
            {
                enemies.Remove(enemy);
                enemy.PictureBox.Visible = false;
            }

            // Çarpışma kontrolü: Oyuncunun uzay aracı ile düşmanların çarpışması
            CollisionDetector.CheckCollision(oyuncu, enemies, this);
        }

        // Yeni düşmanları ekleyen zamanlayıcı
        private void EnemySpawnTimer_Tick(object sender, EventArgs e)
        {
            Random rand = new Random();
            int randomX = rand.Next(formGenislik - 100, formGenislik - 100);
            int randomY = rand.Next(50, formYukseklik - 50);

            // Düşman türünü rastgele seç
            int enemyType = rand.Next(0, 4);

            Enemy newEnemy = null;
            switch (enemyType)
            {
                case 0: // BasicEnemy
                    newEnemy = new BasicEnemy(randomX, randomY, 10, 2, 20);
                    break;
                case 1: // FastEnemy
                    newEnemy = new FastEnemy(randomX, randomY, 10, 8, 10);
                    break;
                case 2: // StrongEnemy
                    newEnemy = new StrongEnemy(randomX, randomY, 20, 5, 20);
                    break;
                case 3: // BossEnemy
                    newEnemy = new BossEnemy(randomX, randomY, 30, 5, 20);
                    break;
            }

            // Düşmanı listeye ekle ve PictureBox oluştur
            if (newEnemy != null)
            {
                enemies.Add(newEnemy);
                PictureBox newEnemyBox = new PictureBox
                {
                    Size = new Size(50, 50),
                    Location = new Point(newEnemy.X, newEnemy.Y),
                    Parent = pictureBoxGalaxy
                };
                newEnemyBox.SizeMode = PictureBoxSizeMode.StretchImage;
                newEnemyBox.Image = newEnemy.GetImage();
                newEnemy.PictureBox = newEnemyBox; // PictureBox referansını düşmana bağlamak
            }
        }
        // Oyuncu mermisi hareketi
        private void BulletMoveTimer_Tick(object sender, EventArgs e)
        {
            if (playerBullet != null && playerBullet.PictureBox != null)
            {
                playerBullet.Move();
                playerBullet.PictureBox.Location = new Point(playerBullet.PositionX, playerBullet.PositionY);

                List<Enemy> enemiesToRemove = new List<Enemy>();

                foreach (var enemy in enemies)
                {
                    if (playerBullet.Bounds.IntersectsWith(enemy.Bounds))
                    {
                        enemy.TakeDamage(playerBullet.Damage);
                        if (enemy.Health <= 0)
                        {
                            AddEnemyKill(enemy.GetType().Name);
                            enemiesToRemove.Add(enemy);
                            enemy.PictureBox.Visible = false;
                        }
                        playerBullet.OnHit();
                        playerBullet = null;  // Mermi yok olur
                        break;
                    }
                }

                // Ölü düşmanları listeden çıkar
                foreach (var enemy in enemiesToRemove)
                {
                    enemies.Remove(enemy);
                }

                // Ekran dışına çıkan mermiyi yok et
                if (playerBullet != null && (playerBullet.PositionX < 0 || playerBullet.PositionX > this.Width || playerBullet.PositionY < 0 || playerBullet.PositionY > this.Height))
                {
                    playerBullet.PictureBox.Visible = false;
                    playerBullet = null;
                }
            }
        }
        // Düşman mermileri hareketi
        private void EnemyShootTimer_Tick(object sender, EventArgs e)
        {
            // Düşmanlar için mermi atma işlemi
            foreach (var enemy in enemies)
            {
                Bullet enemyBullet = enemy.Shoot();
                enemyBullets.Add(enemyBullet);

                // Düşman mermisinin PictureBoxını oluştur
                PictureBox enemyBulletBox = new PictureBox
                {
                    Size = new Size(15, 15),
                    Location = new Point(enemyBullet.PositionX, enemyBullet.PositionY),
                    BackColor = Color.Green,
                    Parent = pictureBoxGalaxy
                };
                enemyBullet.PictureBox = enemyBulletBox;
            }
        }

        private void EnemyBulletMoveTimer_Tick(object sender, EventArgs e)
        {
            for (int i = enemyBullets.Count - 1; i >= 0; i--)
            {
                Bullet bullet = enemyBullets[i];

                // Mermiyi hareket ettir
                bullet.Move();

                // Hareketi PictureBoxa uygula
                if (bullet.PictureBox != null)
                {
                    bullet.PictureBox.Location = new Point(bullet.PositionX, bullet.PositionY);
                }

                // Düşman mermisinin oyuncuya çarpıp çarpmadığını kontrol et
                if (bullet.Bounds.IntersectsWith(oyuncu.Bounds))
                {
                    oyuncu.TakeDamage(bullet.Damage);  // Oyuncu hasar alır
                    enemyBullets.RemoveAt(i);  // Düşman mermisini yok et
                    bullet.PictureBox.Visible = false;  // Mermiyi görünmez yap
                    if (oyuncu.Health <= 0)
                    {
                        AddEnemyKill(bullet.GetType().Name);
                        EndGame($"Oyuncu öldü. Skorunuz: {playerScore}");
                    }
                    break;
                }

                // Mermi ekran dışına çıktıysa sil
                if (bullet.PositionX < 0 || bullet.PositionX > formGenislik ||
                    bullet.PositionY < 0 || bullet.PositionY > formYukseklik)
                {
                    bullet.PictureBox.Visible = false;
                    bullet.PictureBox.Dispose();
                    enemyBullets.RemoveAt(i);
                }
            }
        }
        private void PowerUpMoveTimer_Tick(object sender, EventArgs e)
        {
            // PowerUp listesindeki her bir öğeyi hareket ettir
            for (int i = 0; i < powerUps.Count; i++)
            {
                var powerUp = powerUps[i];

                if (powerUp != null && powerUp.PictureBox != null)
                {
                    // PowerUp'ı sola hareket ettir
                    powerUp.X -= Powerupspeed; // X koordinatını güncelle

                    // PowerUp'ın PictureBox konumunu güncelle
                    powerUp.PictureBox.Location = new Point(powerUp.X, powerUp.Y);

                    // PowerUp oyuncu ile çarpıştıysa sağlık artır
                    if (oyuncu.Bounds.IntersectsWith(powerUp.PictureBox.Bounds))
                    {
                        oyuncu.Health += 10; // Örneğin 10 sağlık artışı
                        powerUp.PictureBox.Visible = false; // PowerUp'ı yok et
                        powerUps.RemoveAt(i); // Listeden çıkar
                        i--; // Listeden eleman çıkardıkça i'yi azaltmak
                    }

                    // Ekrandan çıktıysa power-up'ı kaldır
                    if (powerUp.X < 0)
                    {
                        powerUp.PictureBox.Visible = false;
                        powerUp.PictureBox.Dispose();
                        powerUps.RemoveAt(i); // Power-up'ı listeden çıkar

                        // Listede eleman çıkardığınız için, i'yi bir azaltıyoruz
                        i--;
                    }
                }
            }
        }
        private void PowerUpTimer_Tick(object sender, EventArgs e)
        {
            // Yeni bir PowerUp nesnesi oluştur
            PowerUp newPowerUp = new PowerUp(rand.Next(formGenislik - 200, formGenislik - 50), rand.Next(50, formYukseklik - 50),50);

            // PowerUp'ı listeye ekle
            powerUps.Add(newPowerUp);

            // PowerUp'ı PictureBox olarak başlat
            newPowerUp.PictureBox.Size = new Size(30, 30);
            newPowerUp.PictureBox.Location = new Point(newPowerUp.X, newPowerUp.Y);
            newPowerUp.PictureBox.Image = Properties.Resources.guclendir; // Resmi ayarlayın
            newPowerUp.PictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            newPowerUp.PictureBox.Parent = pictureBoxGalaxy;
            newPowerUp.PictureBox.Visible = true; // Görünür yap
        }
       
        //Bu metot kullanıcı klavye tuşlarına basıldığında çağrılır. 
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            oyuncu.Speed = 10; // Hareket hızı
            if (e.KeyCode == Keys.Up)
            {
                oyuncu.Move(Direction.Up);
                GuncelleKonum();
            }
            else if (e.KeyCode == Keys.Down)
            {
                oyuncu.Move(Direction.Down);
                GuncelleKonum();
            }
            else if (e.KeyCode == Keys.Left)
            {
                oyuncu.Move(Direction.Left);
                GuncelleKonum();
            }
            else if (e.KeyCode == Keys.Right)
            {
                oyuncu.Move(Direction.Right);
                GuncelleKonum();
            }
            else if (e.KeyCode == Keys.Space && playerBullet == null)
            {
                if (playerBullet == null)
                {
                    playerBullet = oyuncu.Shoot(Direction.Right);
                    // Oyuncu mermi atıyor

                    // pictureBox6'yı mermi olarak tanımla
                    pictureBox5.Size = new Size(10, 10);
                    pictureBox5.Location = new Point(playerBullet.PositionX, playerBullet.PositionY);
                    pictureBox5.BackColor = Color.Red;
                    pictureBox5.Visible = true;

                    // playerBulletın PictureBoxını playerBullet nesnesine bağla
                    playerBullet.PictureBox = pictureBox5;
                }
            }
        }
        //Bu metot kullanıcı tuşu bıraktığında çağrılır.
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                oyuncu.StopMovement();
                GuncelleKonum();
            }
        }
        //GuncelleKonum fonksiyonu ile oyuncunun yeni konumu güncellenir.
        private void GuncelleKonum()
        {
            SpaceShip.Location = new Point(oyuncu.X, oyuncu.Y);
            UpdateLabels();

        }
        //Labeller güncellenir
        public void UpdateLabels()
        {
            label1.Text = $"Oyuncu Sağlık: {oyuncu.Health}";
            label6.Text = $"Düşman Sayısı: {enemies.Count}";
        }

    }
}