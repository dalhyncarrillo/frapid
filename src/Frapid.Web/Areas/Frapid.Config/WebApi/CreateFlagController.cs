// ReSharper disable All
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using Frapid.ApplicationState.Cache;
using Frapid.ApplicationState.Models;
using Frapid.DataAccess;
using Frapid.DataAccess.Models;
using Frapid.Framework;
using Frapid.Framework.Extensions;
using Frapid.WebApi;
using Frapid.Config.Entities;
using Frapid.Config.DataAccess;
namespace Frapid.Config.Api
{
    /// <summary>
    /// Provides a direct HTTP access to execute the function CreateFlag.
    /// </summary>
    [RoutePrefix("api/v1.0/config/procedures/create-flag")]
    public class CreateFlagController : FrapidApiController
    {
        /// <summary>
        ///     The CreateFlag repository.
        /// </summary>
        private ICreateFlagRepository repository;

        public class Annotation
        {
            public int UserId { get; set; }
            public int FlagTypeId { get; set; }
            public string Resource { get; set; }
            public string ResourceKey { get; set; }
            public string ResourceId { get; set; }
        }


        public CreateFlagController()
        {
        }

        public CreateFlagController(ICreateFlagRepository repository)
        {
            this.repository = repository;
        }

        protected override void Initialize(HttpControllerContext context)
        {
            base.Initialize(context);
            if (this.repository == null)
            {
                this.repository = new CreateFlagProcedure
                {
                    _Catalog = this.MetaUser.Catalog,
                    _LoginId = this.MetaUser.LoginId,
                    _UserId = this.MetaUser.UserId
                };
            }
        }
        /// <summary>
        ///     Creates meta information of "create flag" annotation.
        /// </summary>
        /// <returns>Returns the "create flag" annotation meta information to perform CRUD operation.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("annotation")]
        [Route("~/api/config/procedures/create-flag/annotation")]
        [RestAuthorize]
        public EntityView GetAnnotation()
        {
            return new EntityView
            {
                Columns = new List<EntityColumn>()
                {
                        new EntityColumn
                        {
                                ColumnName = "user_id_",
                                PropertyName = "UserId",
                                DataType = "int",
                                DbDataType = "integer",
                                IsNullable = false,
                                IsPrimaryKey = false,
                                IsSerial = false,
                                Value = "",
                                MaxLength = 0
                        },
                        new EntityColumn
                        {
                                ColumnName = "flag_type_id_",
                                PropertyName = "FlagTypeId",
                                DataType = "int",
                                DbDataType = "integer",
                                IsNullable = false,
                                IsPrimaryKey = false,
                                IsSerial = false,
                                Value = "",
                                MaxLength = 0
                        },
                        new EntityColumn
                        {
                                ColumnName = "resource_",
                                PropertyName = "Resource",
                                DataType = "string",
                                DbDataType = "text",
                                IsNullable = false,
                                IsPrimaryKey = false,
                                IsSerial = false,
                                Value = "",
                                MaxLength = 0
                        },
                        new EntityColumn
                        {
                                ColumnName = "resource_key_",
                                PropertyName = "ResourceKey",
                                DataType = "string",
                                DbDataType = "text",
                                IsNullable = false,
                                IsPrimaryKey = false,
                                IsSerial = false,
                                Value = "",
                                MaxLength = 0
                        },
                        new EntityColumn
                        {
                                ColumnName = "resource_id_",
                                PropertyName = "ResourceId",
                                DataType = "string",
                                DbDataType = "text",
                                IsNullable = false,
                                IsPrimaryKey = false,
                                IsSerial = false,
                                Value = "",
                                MaxLength = 0
                        }
                }
            };
        }


        /// <summary>
        ///     Creates meta information of "create flag" entity.
        /// </summary>
        /// <returns>Returns the "create flag" meta information to perform CRUD operation.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("meta")]
        [Route("~/api/config/procedures/create-flag/meta")]
        [RestAuthorize]
        public EntityView GetEntityView()
        {
            return new EntityView
            {
                Columns = new List<EntityColumn>()
                {
                }
            };
        }


        [AcceptVerbs("POST")]
        [Route("execute")]
        [Route("~/api/config/procedures/create-flag/execute")]
        [RestAuthorize]
        public void Execute([FromBody] Annotation annotation)
        {
            try
            {
                this.repository.UserId = annotation.UserId;
                this.repository.FlagTypeId = annotation.FlagTypeId;
                this.repository.Resource = annotation.Resource;
                this.repository.ResourceKey = annotation.ResourceKey;
                this.repository.ResourceId = annotation.ResourceId;


                this.repository.Execute();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch (DataAccessException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    Content = new StringContent(ex.Message),
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
#if !DEBUG
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
#endif
        }
    }
}