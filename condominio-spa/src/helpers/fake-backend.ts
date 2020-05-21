import { Injectable } from '@angular/core';
import { HttpRequest, HttpResponse, HttpHandler, HttpEvent, HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { delay, mergeMap, materialize, dematerialize } from 'rxjs/operators';
import { User } from '../models/user';

const users: User[] = [
  { id: 1, username: 'vitor', password: '123', firstName: 'Vitor', lastName: 'Del Sarto' }
];

@Injectable()
export class FakeBackendInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const { url, method, headers, body } = request;
        const fakeToken = 'FAKEGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c';

        return of(null)
            .pipe(mergeMap(handleRoute))
            .pipe(materialize())
            .pipe(delay(500))
            .pipe(dematerialize());

        function handleRoute() {
            switch (true) {
                case url.endsWith('/authenticate') && method === 'POST':
                    return login();
                default:
                    return next.handle(request);
            }
        }
        function login() {
            const { username, password } = body;

            const user = users.find(x => x.username === username && x.password === password);
            if (!user) { return error('Username or password is incorrect'); }

            return ok({
                id: user.id,
                username: user.username,
                firstName: user.firstName,
                lastName: user.lastName,
                token: fakeToken
            });
        }

        function getUsers() {
            if (!isLoggedIn()) { return unauthorized(); }
            return ok(users);
        }

        function ok(body?) {
            return of(new HttpResponse({ status: 200, body }));
        }

        function error(message) {
            return throwError({ error: { message } });
        }

        function unauthorized() {
            return throwError({ status: 401, error: { message: 'Unauthorized' } });
        }

        function isLoggedIn() {
            return headers.get('Authorization') === 'Bearer ' + fakeToken;
        }
    }
}

export let fakeBackendProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: FakeBackendInterceptor,
    multi: true
};
