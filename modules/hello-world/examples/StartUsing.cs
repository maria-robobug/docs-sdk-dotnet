﻿using System;
// #tag::using[]
using System.Threading.Tasks;
using Couchbase;
// #end::using[]

namespace examples
{
    class StartUsing
    {
        static async Task Main(string[] args)
        {

            // #tag::connect[]
             var cluster = await Cluster.ConnectAsync("couchbase://localhost", "username", "password");
            // #end::connect[]


            // #tag::bucket[]
            // get a bucket reference
            var bucket = await cluster.BucketAsync("bucket-name");
            // #end::bucket[]

            // #tag::collection[]
            // get a collection reference
            var collection = bucket.DefaultCollection();
            // #end::collection[]

            // #tag::upsert-get[]
            // Upsert Document
            var upsertResult = await collection.UpsertAsync("my-document-key", new { Name = "Ted", Age = 31 });
            var getResult = await collection.GetAsync("my-document-key");

            Console.WriteLine(getResult.ContentAs<dynamic>());
            // #end::upsert-get[]

            // #tag::n1ql-query[]
            var queryResult = await cluster.QueryAsync<dynamic>("select \"Hello World\" as greeting");
            await foreach (var row in queryResult) {
                Console.WriteLine(row);
            }
            // #end::n1ql-query[]

        }
    }
}
