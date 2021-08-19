import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Member } from '../_models/member';
import { of, pipe } from 'rxjs';
import { map, take } from 'rxjs/operators';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';
import { User } from '../_models/user';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Post } from '../_models/post';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  baseUrl = environment.apiUrl;
  posts: Post[] = [];
  memberCache = new Map();
  postCache = new Map();
  user: User;
  userParams: UserParams;

  constructor(private http: HttpClient, private accountService: AccountService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => {
      this.user = user;
      this.userParams = new UserParams(user);
    })
  }

  getPostsByUsername(username: string) {

    let params = getPaginationHeaders(this.userParams.pageNumber, this.userParams.pageSize);

    return getPaginatedResult<Post[]>(this.baseUrl + 'posts/' + username, params, this.http)
      .pipe(map(response => {
        this.postCache.set(Object.values(this.userParams).join('-'), response);
        return response;
      }))
  }

  /*
  getPost(postId: number) {
    const post = [...this.postCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((post: Post) => post.id === postId);

    if (post) {
      return of(post);
    }
    return this.http.get<Post>(this.baseUrl + 'posts/' + postId);
  }
  */
  deletePost(postId: number) {
    return this.http.delete(this.baseUrl + 'posts/remove/' + postId);
  }

  addLike(postId: number) {
    return this.http.post(this.baseUrl + 'likes/' + postId, {})
  }

  
}
