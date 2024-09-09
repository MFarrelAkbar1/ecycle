using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecycle
{
    class Review
    {
        public int reviewID
        {
            get;
            private set;
        }
        public int produkID
        {
            get;
            private set;
        }
        public int penggunaID
        {
            get;
            private set;
        }
        public int rating
        {
            get;
            private set;
        }
        public string komentar
        {
            get;
            private set;
        }
        public DateTime tanggal
        {
            get;
            private set;
        }

        public void tambahReview(int reviewID, int produkID, int penggunaID, int rating, string komentar, DateTime tanggal)
        {
            this.reviewID = reviewID;
            this.produkID = produkID;
            this.penggunaID = penggunaID;
            this.rating = rating;
            this.komentar = komentar;
            this.tanggal = tanggal;
        }
        public void hapusReview(int reviewID, int produkID, int penggunaID, int rating, string komentar, DateTime tanggal)
        {
            this.reviewID = reviewID;
            this.produkID = produkID;
            this.penggunaID = penggunaID;
            this.rating = rating;
            this.komentar = komentar;
            this.tanggal = tanggal;
        }
        public void updateReview(int reviewID, int produkID, int penggunaID, int rating, string komentar, DateTime tanggal)
        {
            this.reviewID = reviewID;
            this.produkID = produkID;
            this.penggunaID = penggunaID;
            this.rating = rating;
            this.komentar = komentar;
            this.tanggal = tanggal;
        }

    }
}
