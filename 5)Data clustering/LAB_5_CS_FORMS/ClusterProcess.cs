using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab5AI
{
    class ClusterProcess
    {
            // (4):
        // Реалізувати допоміжну функцію для виконання алгоритму кластеризації за методом К-середніх( за допомогою ФУНКЦІЇ-ЦЕНТРОЇД ):
        public static List<int> ClusterKMeans(List<Point> data, int numClusters, int seed)
        {
            bool changed = true; bool success = true;
            List<int> clustering = InitClustering(data.Count, numClusters, seed);

            List<Point> means = Allocate(numClusters);
            int maxCount = data.Count * Const.Time;

            int ct = 0;
            while (changed == true && success == true && ct < maxCount)
            {
                ++ct;
                success = UpdateMeans(data, clustering,ref means);
                changed = UpdateClustering(data, ref clustering, means);
                Debug.WriteLine("ct: " + ct + " success mean: " + success + " changed cluster: " + changed);

            }
            return clustering;
        }


        private static List<int> InitClustering(int numTuples, int numClusters, int seed=0)
        {
            Random random = new Random(seed);
            List<int> clustering = new List<int>(); ;
            for (int i = 0; i < numClusters; ++i)
                clustering.Add(i);
            for (int i = numClusters; i < numTuples; ++i)
                clustering.Add(random.Next(0, numClusters));
        
            return clustering;
        }


        // Оновити значення:
        private static bool UpdateMeans(List<Point> data, List<int> clustering, ref List<Point> means)
        {
            int numClusters = means.Count;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Count; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
            {
                if (clusterCounts[k] == 0)
                    return false;
            }


            for (int k = 0; k < means.Count; ++k)
                    means[k].Clear();

            for (int i = 0; i < data.Count; ++i)
            {
                int cluster = clustering[i];
                // накопичується сума:
                means[cluster] = means[cluster]+data[i]; 
            }

            for (int k = 0; k < means.Count; ++k)
            {
                // небезпека різноманітності 0:
                means[k] = means[k] / clusterCounts[k]; 
                Debug.Write(means[k]);
            }
            Debug.WriteLine(" ");
            return true;
        }


        private static List<Point> Allocate(int numClusters)
        {
            List<Point> result = new List<Point>();
            for (int k = 0; k < numClusters; ++k)
                result.Add(new Point());
            return result;
        }


            // (3):
        //  Реалізувати допоміжну функцію для обчислення міри віддалі:
        private static double Distance(Point tuple, Point mean)
        {
            return Point.CurrentMetric(tuple, mean);
        }


            // (6):
        // Безпосередньо реалізувати кластеризацію даних двома методами та порівняти результати кластеризації:
        private static bool UpdateClustering(List<Point> data, ref List<int> clustering, List<Point> means)
        {
            int numClusters = means.Count;
            bool changed = false;

            List<int> newClustering = new List<int>(clustering.Count);
            newClustering = clustering;

            double[] distances = new double[numClusters];

            for (int i = 0; i < data.Count; ++i)
            {
                for (int k = 0; k < numClusters; ++k)
                    distances[k] = Distance(data[i], means[k]);

                int newClusterID = MinIndex(distances);
                if (newClusterID != newClustering[i])
                {
                    Debug.WriteLine(newClusterID + "!=" + newClustering[i]);
                    changed = true;
                    newClustering[i] = newClusterID;
                }
            }

            if (changed == false)
                return false;

            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < data.Count; ++i)
            {
                int cluster = newClustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
            {
                Debug.Write("Cluster" + k + ": " + clusterCounts[k] + ", ");
                if (clusterCounts[k] == 0)
                    return false;
            }
            Debug.WriteLine(" ");
            // немає нульових рахунків і принаймні одна зміна:
            clustering = newClustering;
            return true; 
        }


        private static int MinIndex(double[] distances)
        {
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }
            }
            return indexOfMin;
        }


            // (7):
        // Порівняйте кількість кластерів та якість кластеризації (можна просто оцінити середньо-зважене  
        // розмірів утворених кластерів згідно заданої міри віддалі для кожного із методів:
        public static List<int> ClusterMeanShift(List<Point> data,double radius=0, int radiusStep=5)
        {
            if (radius == 0)
            {
                double x = data.Average(point => point.x);
                double y = data.Average(point => point.y);

                double allDataNorm = Point.Norm(new Point(x, y));
                radius = allDataNorm / radiusStep;
                Debug.WriteLine(radius);
            }
            List<Point> centroids = data;
            List<int> weight = new List<int>();
            for(int i=radiusStep-1;i<=0;--i)
            {
                weight.Add(i);
            }
            while(true)
            {
                List<Point> newCentroids = new List<Point>();
                for(int i=0;i<centroids.Count;++i)
                {
                    List<Point> inBandWidth = new List<Point>();

                    for (int j = 0; j < data.Count; ++j)
                    {
                        double distance = Point.Norm(data[j] - centroids[i]);

                        if (distance == 0)
                        distance = 0.00000000001;
                        int weightIndex = Convert.ToInt32(distance / radius);
                        if (weightIndex > radiusStep - 1)
                        weightIndex = radiusStep - 1;

                        for (int k = 0; i < weight[weightIndex] * weight[weightIndex]; ++i)
                            inBandWidth.Add(data[i]);

                    }
                    newCentroids.Add(new Point(data.Average(point => point.x), data.Average(point => point.y)));
                    newCentroids.Sort();
                    var uniques = newCentroids;

                    List<Point> toPop = new List<Point>();

                    #region MyRegion
                    //            for(int k=0;i<uniques.Count;++i)
                    //            {
                    //                for(int kk=0;)
                    //            }

                    //    for i in uniques:
                    //        for ii in [i for i in uniques]:
                    //            if i == ii:
                    //                pass
                    //            elif np.linalg.norm(np.array(i) - np.array(ii)) <= self.radius:
                    //                # print(np.array(i), np.array(ii))
                    //                to_pop.append(ii)
                    //                break

                    //    for i in to_pop:
                    //        try:
                    //            uniques.remove(i)
                    //        except:
                    //                pass

                    //        prev_centroids = dict(centroids)
                    //    centroids = { }
                    //                for i in range(len(uniques)):

                    //                    centroids[i] = np.array(uniques[i])


                    //                optimized = True

                    //    for i in centroids:
                    //        if not np.array_equal(centroids[i], prev_centroids[i]):
                    //            optimized = False

                    //    if optimized:
                    //        break

                    //self.centroids = centroids
                    //self.classifications = { }

                    //                for i in range(len(self.centroids)):

                    //                    self.classifications[i] = []

                    //for featureset in data:
                    //    # compare distance to either centroid
                    //    distances = [np.linalg.norm(featureset - self.centroids[centroid]) for centroid in self.centroids]
                    //    # print(distances)
                    //    classification = (distances.index(min(distances)))

                    //    # featureset that belongs to that cluster
                    //    self.classifications[classification].append(featureset) 
                    #endregion
                }
            }
        }
        #region MyRegion
        /*

    def fit(self, data):


        while True:
            new_centroids = []
            for i in centroids:
                in_bandwidth = []
                centroid = centroids[i]

                for featureset in data:

                    distance = np.linalg.norm(featureset - centroid)
                    if distance == 0:
                        distance = 0.00000000001
                    weight_index = int(distance / self.radius)
                    if weight_index > self.radius_norm_step - 1:
                        weight_index = self.radius_norm_step - 1

                    to_add = (weights[weight_index] ** 2) * [featureset]
                    in_bandwidth += to_add

                new_centroid = np.average(in_bandwidth, axis=0)
                new_centroids.append(tuple(new_centroid))

            uniques = sorted(list(set(new_centroids)))

            to_pop = []

            for i in uniques:
                for ii in [i for i in uniques]:
                    if i == ii:
                        pass
                    elif np.linalg.norm(np.array(i) - np.array(ii)) <= self.radius:
                        # print(np.array(i), np.array(ii))
                        to_pop.append(ii)
                        break

            for i in to_pop:
                try:
                    uniques.remove(i)
                except:
                    pass

            prev_centroids = dict(centroids)
            centroids = {}
            for i in range(len(uniques)):
                centroids[i] = np.array(uniques[i])

            optimized = True

            for i in centroids:
                if not np.array_equal(centroids[i], prev_centroids[i]):
                    optimized = False

            if optimized:
                break

        self.centroids = centroids
        self.classifications = {}

        for i in range(len(self.centroids)):
            self.classifications[i] = []

        for featureset in data:
            # compare distance to either centroid
            distances = [np.linalg.norm(featureset - self.centroids[centroid]) for centroid in self.centroids]
            # print(distances)
            classification = (distances.index(min(distances)))

            # featureset that belongs to that cluster
            self.classifications[classification].append(featureset)

    def predict(self, data):
        # compare distance to either centroid
        distances = [np.linalg.norm(data - self.centroids[centroid]) for centroid in self.centroids]
        classification = (distances.index(min(distances)))
        return classification


if __name__ == "__main__":
    # Generate data using sklean build in function
    data = make_blobs(n_samples=45, n_features=2, centers=3)
    print("Data:", data[0][0], "class:", data[1][0])

    x = []
    y = []
    for elem in data[0]:
        x.append(elem[0])
        y.append(elem[1])

    clf = Mean_Shift()
    clf.fit(data[0])
    centers = clf.centroids

    colors = 10 * ['r', 'g', 'b', 'c', 'k', 'y']
    for classification in clf.classifications:
        color = colors[classification]
        for featureset in clf.classifications[classification]:
            plt.scatter(featureset[0], featureset[1], marker="x", color=color, s=150, linewidths=5, zorder=10)

    for c in centers:
        plt.scatter(centers[c][0], centers[c][1], color='k', marker="*", s=150, linewidths=5)

    plt.show()
         */

        #endregion
    }
}